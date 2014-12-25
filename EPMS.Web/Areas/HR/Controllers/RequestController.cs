using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Request;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EmployeeRequest = EPMS.Web.Models.EmployeeRequest;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    public class RequestController : BaseController
    {
        private readonly IEmployeeRequestService employeeRequestService;
        private readonly IAspNetUserService aspNetUserService;
        public RequestController(IEmployeeRequestService employeeRequestService, IAspNetUserService aspNetUserService)
        {
            this.employeeRequestService = employeeRequestService;
            this.aspNetUserService = aspNetUserService;
        }

        // GET: HR/Request
        public ActionResult Index()
        {
            EmployeeRequestSearchRequest searchRequest = Session["PageMetaData"] as EmployeeRequestSearchRequest;

            Session["PageMetaData"] = null;

            EmployeeRequestViewModel viewModel = new EmployeeRequestViewModel();
            
            viewModel.SearchRequest = searchRequest ?? new EmployeeRequestSearchRequest();
            
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(EmployeeRequestSearchRequest searchRequest)
        {
            EmployeeRequestViewModel viewModel = new EmployeeRequestViewModel();
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            if (userRole != null && userRole.Name == "Admin")
            {
                searchRequest.Requester = "Admin";
            }
            else
            {
                searchRequest.Requester = aspNetUserService.FindById(User.Identity.GetUserId()).EmployeeId.ToString();
            }
            var employeeRequestResponse = employeeRequestService.LoadAllRequests(searchRequest);
            var data = employeeRequestResponse.EmployeeRequests.Select(x => x.CreateFromServerToClient());
            var employeeRequests = data as IList<EmployeeRequest> ?? data.ToList();
            if (employeeRequests.Any())
            {
                viewModel.aaData = employeeRequests;
                viewModel.iTotalRecords = employeeRequestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = employeeRequestResponse.EmployeeRequests.Count();
                viewModel.sEcho = employeeRequestResponse.EmployeeRequests.Count();
            }
            else
            {
                viewModel.aaData = Enumerable.Empty<EmployeeRequest>();
                viewModel.iTotalRecords = employeeRequestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = employeeRequestResponse.EmployeeRequests.Count();
                viewModel.sEcho = 1;
            }
            // Keep Search Request in Session
            Session["PageMetaData"] = searchRequest;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        // GET: HR/Request/Create/RequestId
        public ActionResult Create(long? id)
        {
            EmployeeRequestViewModel requestViewModel = new EmployeeRequestViewModel();
            AspNetUser currentUser = aspNetUserService.FindById(User.Identity.GetUserId());
            if (User.Identity.GetUserId() != null)
            {
                if (id != null)
                {
                    var employeeRequest = employeeRequestService.Find((long)id);
                    //Check if current request is related to logedin user.
                    if (employeeRequest.EmployeeId == currentUser.EmployeeId || currentUser.AspNetRoles.FirstOrDefault().Name=="Admin")
                    {
                        requestViewModel.EmployeeRequest = employeeRequest.CreateFromServerToClient();
                        requestViewModel.EmployeeRequestDetail = employeeRequestService.LoadRequestDetailByRequestId((long)id).CreateFromServerToClient();
                    }
                    ViewBag.UserRole = currentUser.AspNetRoles.FirstOrDefault().Name;
                }
                if (currentUser.Employee != null)
                {
                    if (currentUser.Employee.EmployeeId > 0)
                    {
                        requestViewModel.EmployeeRequest.Employee = currentUser.Employee.CreateFromServerToClient();
                        requestViewModel.EmployeeRequest.Employee.DepartmentNameE = currentUser.Employee.JobTitle.Department.DepartmentNameE;
                        requestViewModel.EmployeeRequest.Employee.DepartmentNameA = currentUser.Employee.JobTitle.Department.DepartmentNameA;
                    }
                }
            }
            if (requestViewModel.EmployeeRequestDetail.IsApproved)
                ViewBag.MessageVM = new MessageViewModel { Message = "Your request has been approved by the Administrator, now you are unable to make changes in this request.", IsInfo = true };
            return View(requestViewModel);
        }
        // Post: HR/Request/Create
        [HttpPost]
        public ActionResult Create(EmployeeRequestViewModel requestViewModel)
        {
            try
            {
                requestViewModel.EmployeeRequest.EmployeeId = requestViewModel.EmployeeRequest.Employee.EmployeeId;
                //update
                if (requestViewModel.EmployeeRequest.RequestId > 0)
                {
                    requestViewModel.EmployeeRequest.RecLastUpdatedBy = User.Identity.GetUserId();
                    requestViewModel.EmployeeRequest.RecLastUpdatedDt = DateTime.Now;
                    //Update Request
                    if (employeeRequestService.UpdateRequest(requestViewModel.EmployeeRequest.CreateFromClientToServer()))
                    {
                        //Add RequestDetail
                        requestViewModel.EmployeeRequestDetail.RequestId = requestViewModel.EmployeeRequest.RequestId;
                        requestViewModel.EmployeeRequestDetail.RecCreatedBy = User.Identity.GetUserId();
                        requestViewModel.EmployeeRequestDetail.RecCreatedDt = DateTime.Now;
                        requestViewModel.EmployeeRequestDetail.RecLastUpdatedBy = User.Identity.GetUserId();
                        requestViewModel.EmployeeRequestDetail.RecLastUpdatedDt = DateTime.Now;
                        requestViewModel.EmployeeRequestDetail.RowVersion++;
                        employeeRequestService.AddRequestDetail(requestViewModel.EmployeeRequestDetail.CreateFromClientToServer());
                        TempData["message"] = new MessageViewModel { Message = "Request has been updated.", IsUpdated = true };
                    }
                }
                // create new
                else
                {
                    requestViewModel.EmployeeRequest.RequestDate = DateTime.Now;

                    requestViewModel.EmployeeRequest.RecCreatedBy = User.Identity.GetUserId();
                    requestViewModel.EmployeeRequest.RecCreatedDt = DateTime.Now;
                    requestViewModel.EmployeeRequest.RecLastUpdatedBy = User.Identity.GetUserId();
                    requestViewModel.EmployeeRequest.RecLastUpdatedDt = DateTime.Now;

                    //Add Request to Db, and get RequestId
                    requestViewModel.EmployeeRequest.RequestId = employeeRequestService.AddRequest(requestViewModel.EmployeeRequest.CreateFromClientToServer());
                    if (requestViewModel.EmployeeRequest.RequestId > 0)
                    {
                        //Add RequestDetail
                        requestViewModel.EmployeeRequestDetail.RequestId = requestViewModel.EmployeeRequest.RequestId;
                        requestViewModel.EmployeeRequestDetail.RecCreatedBy = User.Identity.GetUserId();
                        requestViewModel.EmployeeRequestDetail.RecCreatedDt = DateTime.Now;
                        requestViewModel.EmployeeRequestDetail.RecLastUpdatedBy = User.Identity.GetUserId();
                        requestViewModel.EmployeeRequestDetail.RecLastUpdatedDt = DateTime.Now;
                        employeeRequestService.AddRequestDetail(requestViewModel.EmployeeRequestDetail.CreateFromClientToServer());
                        TempData["message"] = new MessageViewModel { Message = "Request has been created.", IsSaved = true };
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View(requestViewModel);
            }
        }
    }
}