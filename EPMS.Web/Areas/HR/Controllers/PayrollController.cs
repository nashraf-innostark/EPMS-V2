using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Employee;
using EPMS.Web.ViewModels.Payroll;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EPMS.Web.ModelMappers;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IEmployeeRequestService EmployeeRequestService;
        private readonly IEmployeeService EmployeeService;
        private readonly IAspNetUserService AspNetUserService;

        #region Constructor

        public PayrollController(IEmployeeRequestService employeeRequestService, IEmployeeService employeeService, IAspNetUserService aspNetUserService)
        {
            EmployeeRequestService = employeeRequestService;
            EmployeeService = employeeService;
            AspNetUserService = aspNetUserService;
        }

        #endregion

        #region Public

        // GET: HR/Payroll
        public ActionResult Index()
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            if (userRole != null && userRole.Name == "Admin")
            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel
                {
                    SearchRequest = new EmployeeSearchRequset(),
                };
                employeeViewModel.Role = userRole.Name;
                return View(employeeViewModel);
            }
            if (userRole != null && userRole.Name == "Employee")
            {
                long id = AspNetUserService.FindById(User.Identity.GetUserId()).Employee.EmployeeId;
                return RedirectToAction("Detail", new { id });
            }
            return null;
        }

        [HttpPost]
        public ActionResult Index(JQueryDataTableParamModel param)
        {
            EmployeeSearchRequset employeeSearchRequest = new EmployeeSearchRequset();
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            employeeSearchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
// ReSharper disable once SpecifyACultureInStringConversionExplicitly
            employeeSearchRequest.SearchStr = Request["search"].ToString();
            var employees = EmployeeService.GetAllEmployees(employeeSearchRequest);
            IEnumerable<Models.Employee> employeeList =
                employees.Employeess.Select(x => x.CreateFromServerToClientWithImage()).ToList();
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                aaData = employeeList,
                iTotalRecords = Convert.ToInt32(employees.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(employeeList.Count()),
                sEcho = param.sEcho,
            };
            if (userRole != null)
            {
                employeeViewModel.Role = userRole.Name;
            }
            return Json(employeeViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(long id)
        {
            PayrollSearchRequest searchRequest = new PayrollSearchRequest
            {
                UserId = Guid.Parse(User.Identity.GetUserId())
            };
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            if (userRole != null && userRole.Name == "Admin")
            {
                searchRequest.Requester = "Admin";
            }
            else
            {
                searchRequest.Requester = AspNetUserService.FindById(User.Identity.GetUserId()).EmployeeId.ToString();
            }
            PayrollViewModel viewModel = new PayrollViewModel();
            // get employee requests
            var employeeRequestsResponse = EmployeeRequestService.LoadAllMonetaryRequests(DateTime.Now,id);
            var requests = employeeRequestsResponse.Select(x => x.CreateFromServerToClientPayroll());
            // get employee
            viewModel.Employee = EmployeeService.FindEmployeeForPayroll(id,DateTime.Now).Select(x=>x.CreateFromServerToClient());
            //viewModel.Payroll.EmployeeId = viewModel.Employee.EmployeeId;
            //viewModel.Payroll.JobTitle = viewModel.Employee.JobTitle.JobTitleNameE;
            //viewModel.Payroll.BasicSalary = viewModel.Employee.JobTitle.BasicSalary;
            // get employee request details
            foreach (var payroll in requests)
            {
                viewModel.Deduction = payroll.RequestDetails;
            }
            return View(viewModel);
        }
        #endregion
    }
}