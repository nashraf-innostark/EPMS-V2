using System;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Request;
using Microsoft.AspNet.Identity;
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
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View();
        }
        // GET: HR/Request/Create
        public ActionResult Create(long? id)
        {
            EmployeeRequestViewModel requestViewModel = new EmployeeRequestViewModel();
            if (User.Identity.GetUserId() != null)
            {
                AspNetUser currentUser = aspNetUserService.FindById(User.Identity.GetUserId());
                if (id != null)
                {
                    requestViewModel.EmployeeRequest = employeeRequestService.Find((long)id).CreateFromServerToClient();
                    requestViewModel.EmployeeRequestDetail = employeeRequestService.GetRequestDetailByRequestId((long)id).CreateFromServerToClient();
                }
                if (currentUser.Employee != null)
                {
                    if (currentUser.Employee.EmployeeId > 0)
                    {
                        requestViewModel.EmployeeRequest.Employee = currentUser.Employee.CreateFrom();
                        requestViewModel.EmployeeRequest.Employee.DepartmentName = currentUser.Employee.JobTitle.Department.DepartmentName;
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
                return RedirectToAction("Create");
            }
            catch(Exception e)
            {
                return View(requestViewModel);
            }
        }
    }
}