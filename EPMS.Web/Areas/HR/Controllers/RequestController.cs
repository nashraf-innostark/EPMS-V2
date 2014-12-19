using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.ViewModels.Request;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EmployeeRequest = EPMS.Web.Models.EmployeeRequest;

namespace EPMS.Web.Areas.HR.Controllers
{
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
        public ActionResult Create()
        {
            AspNetUser currentUser = aspNetUserService.FindById(User.Identity.GetUserId());
            EmployeeRequestViewModel request = new EmployeeRequestViewModel();
            request.EmployeeName = currentUser.Employee.EmployeeFirstName + " " +
                                   currentUser.Employee.EmployeeMiddleName + " " + currentUser.Employee.EmployeeLastName;
            request.EmployeeDepartment = currentUser.Employee.JobTitle.Department.DepartmentName;
                request.EmployeeRequest = new EmployeeRequest
                {
                    EmployeeId = currentUser.Employee.EmployeeId
                };
            return View(request);
        }
        // Post: HR/Request/Create
        [HttpPost]
        public ActionResult Create(EmployeeRequest request)
        {
            return View();
        }
    }
}