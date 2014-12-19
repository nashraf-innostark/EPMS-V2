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

        public RequestController(IEmployeeRequestService employeeRequestService)
        {
            this.employeeRequestService = employeeRequestService;
        }

        // GET: HR/Request
        public ActionResult Index()
        {
            return View();
        }
        // GET: HR/Request/Create
        public ActionResult Create()
        {
            AspNetUser currentUser = HttpContext.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(User.Identity.GetUserId());
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