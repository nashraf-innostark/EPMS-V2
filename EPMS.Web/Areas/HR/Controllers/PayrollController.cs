using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Employee;
using EPMS.Web.ViewModels.Payroll;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EPMS.Web.ModelMappers;
using Employee = EPMS.Models.DomainModels.Employee;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IEmployeeRequestService EmployeeRequestService;
        private readonly IEmployeeService EmployeeService;
        private readonly IAspNetUserService AspNetUserService;
        private readonly IAllowanceService AllowanceService;

        #region Constructor

        public PayrollController(IEmployeeRequestService employeeRequestService, IEmployeeService employeeService, IAspNetUserService aspNetUserService, IAllowanceService allowanceService)
        {
            EmployeeRequestService = employeeRequestService;
            EmployeeService = employeeService;
            AspNetUserService = aspNetUserService;
            AllowanceService = allowanceService;
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
            long id = AspNetUserService.FindById(User.Identity.GetUserId()).Employee.EmployeeId;
            return RedirectToAction("Detail", new { id });
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
            PayrollViewModel viewModel = new PayrollViewModel();
            // get Employee
            PayrollResponse response = EmployeeService.FindEmployeeForPayroll(id, DateTime.Now);
            if (response.Employee != null)
            {
                viewModel.Employee = response.Employee.CreateFromServerToClient();
            }
            if (response.Allowance != null)
            {
                viewModel.Allowances = response.Allowance.CreateFromServerToClient();
            }
            // get employee requests
            if (response.Requests != null)
            {
                var requests = response.Requests.Select(x => x.CreateFromServerToClientPayroll());
                // get Employee request details
                foreach (var reqDetail in requests)
                {
                    var firstOrDefault = reqDetail.RequestDetails.FirstOrDefault();
                    if (firstOrDefault != null)
                        viewModel.Deduction1 = firstOrDefault.InstallmentAmount ?? 0;
                    var lastOrDefault = reqDetail.RequestDetails.LastOrDefault();
                    if (lastOrDefault != null)
                        viewModel.Deduction2 = lastOrDefault.InstallmentAmount ?? 0;
                }
            }
            double basicSalary=0;
            double allowances=0;
            if (viewModel.Employee != null)
            {
                basicSalary = viewModel.Employee.JobTitle.BasicSalary ?? 0;
            }
            if (viewModel.Allowances != null)
            {
                allowances = viewModel.Allowances.Allowance1 + viewModel.Allowances.Allowance2 + viewModel.Allowances.Allowance3 + viewModel.Allowances.Allowance4 + viewModel.Allowances.Allowance5 ?? 0;
            }
            viewModel.Total = (basicSalary + allowances) - (viewModel.Deduction1 + viewModel.Deduction2);
            return View(viewModel);
        }
        #endregion
    }
}