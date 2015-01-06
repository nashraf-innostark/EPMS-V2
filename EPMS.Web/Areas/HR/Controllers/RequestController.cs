using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Request;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EmployeeRequest = EPMS.Web.Models.Request;

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
        [SiteAuthorize(PermissionKey = "RequestIndex")]
        public ActionResult Index()
        {
            EmployeeRequestSearchRequest searchRequest = Session["PageMetaData"] as EmployeeRequestSearchRequest;
            AspNetUser currentUser = aspNetUserService.FindById(User.Identity.GetUserId());
            ViewBag.UserRole = currentUser.AspNetRoles.FirstOrDefault().Name;
            Session["PageMetaData"] = null;

            RequestListViewModel viewModel = new RequestListViewModel();
            
            viewModel.SearchRequest = searchRequest ?? new EmployeeRequestSearchRequest();
            
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(EmployeeRequestSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            RequestListViewModel viewModel = new RequestListViewModel();
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
            var requestResponse = employeeRequestService.LoadAllRequests(searchRequest);
            var data = requestResponse.EmployeeRequests.Select(x => x.CreateFromServerToClient());
            var employeeRequests = data as IList<EmployeeRequest> ?? data.ToList();
            if (employeeRequests.Any())
            {
                viewModel.aaData = employeeRequests;
                viewModel.iTotalRecords = requestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = requestResponse.TotalCount;
                viewModel.sEcho = searchRequest.sEcho;
                //viewModel.sLimit = searchRequest.iDisplayLength;
            }
            else
            {
                viewModel.aaData = Enumerable.Empty<EmployeeRequest>();
                viewModel.iTotalRecords = requestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = requestResponse.TotalCount;
                viewModel.sEcho = searchRequest.sEcho;
                //viewModel.sLimit = searchRequest.iDisplayLength;
            }
            // Keep Search Request in Session
            Session["PageMetaData"] = searchRequest;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        // GET: HR/Request/Create/RequestId
        [SiteAuthorize(PermissionKey = "RequestCreate")]
        public ActionResult Create(long? id)
        {
            RequestViewModel requestViewModel = new RequestViewModel();
            var userId = User.Identity.GetUserId();
            if (userId == null)
                RedirectToAction("Login", "Account");
            AspNetUser currentUser = aspNetUserService.FindById(User.Identity.GetUserId());
            ViewBag.UserRole = currentUser.AspNetRoles.FirstOrDefault().Name;
            if (User.Identity.GetUserId() != null)
            {
                if (id != null)
                {
                    var request = employeeRequestService.Find((long)id);
                    //Check if current request is related to logedin user.
                    if (request.EmployeeId == currentUser.EmployeeId || ViewBag.UserRole == "Admin")
                    {
                        requestViewModel.Request = request.CreateFromServerToClient();
                        requestViewModel.RequestDetail = request.RequestDetails.FirstOrDefault().CreateFromServerToClient();
                        requestViewModel.RequestDesc = requestViewModel.RequestDetail.RequestDesc;
                        if (request.RequestDetails.Count > 1)
                            requestViewModel.RequestReply = request.RequestDetails.Single(x => x.RowVersion == 1).CreateFromServerToClient();
                    }
                }
                else if (currentUser.EmployeeId > 0)
                {
                    requestViewModel.Request.RequestDate = DateTime.Now;
                    requestViewModel.Request.EmployeeId = Convert.ToInt64(currentUser.EmployeeId);
                    requestViewModel.Request.EmployeeNameA = currentUser.Employee.EmployeeNameA;
                    requestViewModel.Request.EmployeeNameE = currentUser.Employee.EmployeeNameE;
                    requestViewModel.Request.DepartmentNameA = currentUser.Employee.JobTitle.Department.DepartmentNameA;
                    requestViewModel.Request.DepartmentNameE = currentUser.Employee.JobTitle.Department.DepartmentNameE;
                }
            }
            if (requestViewModel.RequestDetail.IsApproved)
                ViewBag.MessageVM = new MessageViewModel { Message = "The request has been accepted, now you are unable to make changes in this request.", IsInfo = true };
            return View(requestViewModel);
        }
        // Post: HR/Request/Create
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(RequestViewModel requestViewModel)
        {
            try
            {
                //update
                if (requestViewModel.Request.RequestId > 0)
                {
                    requestViewModel.RequestReply.IsReplied = true;
                    requestViewModel.RequestReply.RequestId = requestViewModel.Request.RequestId;
                    requestViewModel.RequestReply.RequestDesc = requestViewModel.RequestDesc;
                    requestViewModel.RequestReply.RowVersion = requestViewModel.RequestDetail.RowVersion + 1;
                    requestViewModel.RequestReply.RecCreatedBy = User.Identity.GetUserId();
                    requestViewModel.RequestReply.RecCreatedDt = DateTime.Now;
                    requestViewModel.RequestReply.RecLastUpdatedBy = User.Identity.GetUserId();
                    requestViewModel.RequestReply.RecLastUpdatedDt = DateTime.Now;
                    employeeRequestService.AddRequestDetail(requestViewModel.RequestReply.CreateFromClientToServer());

                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Request has been replied.",
                        IsUpdated = true
                    };
                }
                // create new
                else
                {
                    requestViewModel.Request.RequestDate = DateTime.Now;

                    requestViewModel.Request.RecCreatedBy = User.Identity.GetUserId();
                    requestViewModel.Request.RecCreatedDt = DateTime.Now;
                    requestViewModel.Request.RecLastUpdatedBy = User.Identity.GetUserId();
                    requestViewModel.Request.RecLastUpdatedDt = DateTime.Now;

                    //Add Request to Db, and get RequestId
                    requestViewModel.Request.RequestId = employeeRequestService.AddRequest(requestViewModel.Request.CreateFromClientToServer());
                    if (requestViewModel.Request.RequestId > 0)
                    {
                        //Add RequestDetail
                        requestViewModel.RequestDetail.RequestId = requestViewModel.Request.RequestId;
                        requestViewModel.RequestDetail.RecCreatedBy = User.Identity.GetUserId();
                        requestViewModel.RequestDetail.RecCreatedDt = DateTime.Now;
                        requestViewModel.RequestDetail.RecLastUpdatedBy = User.Identity.GetUserId();
                        requestViewModel.RequestDetail.RecLastUpdatedDt = DateTime.Now;
                        employeeRequestService.AddRequestDetail(requestViewModel.RequestDetail.CreateFromClientToServer());
                        TempData["message"] = new MessageViewModel { Message = "Request has been created.", IsSaved = true };
                    }
                }
                return RedirectToAction("Index","Request");
            }
            catch(Exception e)
            {
                return View(requestViewModel);
            }
        }
        [SiteAuthorize(PermissionKey = "RequestDelete")]
        public ActionResult Delete(long? id)
        {
            if (id > 0)
            {
                bool deleted = employeeRequestService.DeleteRequest((long)id);
                if (deleted)
                {
                    TempData["message"] = new MessageViewModel { Message = "The request has been deleted.", IsInfo = true };
                }
            }
            return RedirectToAction("Index");
        }
    }
}