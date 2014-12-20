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
            return View();
        }
        // GET: HR/Request/Create
        public ActionResult Create(int? id)
        {
            EmployeeRequestViewModel requestViewModel = new EmployeeRequestViewModel();
            if (User.Identity.GetUserId() != null)
            {
                AspNetUser currentUser = aspNetUserService.FindById(User.Identity.GetUserId());

                if (currentUser.Employee != null)
                {
                    requestViewModel.EmployeeRequest.Employee = currentUser.Employee.CreateFrom();
                    requestViewModel.EmployeeRequest.Employee.DepartmentName = currentUser.Employee.JobTitle.Department.DepartmentName;
                }
            }
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
                    //viewModel.Activity.UpdatedBy = User.Identity.GetUserId();
                    //viewModel.Activity.UpdatedDate = DateTime.Now;
                    //if (activityService.Update(viewModel.Activity.CreateFrom()))
                    //{
                    //    TempData["message"] = new MessageViewModel { Message = "Activity has been updated", IsUpdated = true };
                    //    return RedirectToAction("Index");
                    //}
                }
                // create new
                else
                {
                    requestViewModel.EmployeeRequest.RequestDate = DateTime.Now;

                    requestViewModel.EmployeeRequest.RecCreatedBy = User.Identity.GetUserId();
                    requestViewModel.EmployeeRequest.RecCreatedDt = DateTime.Now;
                    requestViewModel.EmployeeRequest.RecLastUpdatedBy = User.Identity.GetUserId();
                    requestViewModel.EmployeeRequest.RecLastUpdatedDt = DateTime.Now;

                    //Add to Db, and get RequestId
                    requestViewModel.EmployeeRequest.RequestId = employeeRequestService.AddRequest(requestViewModel.EmployeeRequest.MapClientToServer());
                    if (requestViewModel.EmployeeRequest.RequestId > 0)
                    {
                        //Add Request Detail

                        TempData["message"] = new MessageViewModel { Message = "New Request has been created", IsSaved = true };
                        return RedirectToAction("Index");
                    }
                }
            }
            catch(Exception e)
            {
            }

            return View();
        }
    }
}