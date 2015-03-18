﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ViewModels.Employee;
using EPMS.Web.ViewModels.Payroll;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EPMS.Web.ModelMappers;
using EPMS.Models.ResponseModels;
using Org.BouncyCastle.Ocsp;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "HRS", IsModule = true)]
    public class PayrollController : BaseController
    {
        private readonly IEmployeeService EmployeeService;
        private readonly IAspNetUserService AspNetUserService;

        #region Constructor

        public PayrollController(IEmployeeService employeeService, IAspNetUserService aspNetUserService)
        {
            EmployeeService = employeeService;
            AspNetUserService = aspNetUserService;
        }

        #endregion

        #region Public
        [SiteAuthorize(PermissionKey = "PayrollIndex")]
        //[SiteAuthorize(PermissionKey = "PayrollIndex")]
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
                    Role = userRole.Name,
                };
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
        public ActionResult Index(EmployeeSearchRequset employeeSearchRequest)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            employeeSearchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
            employeeSearchRequest.SearchString = Request["search"];
            var employees = EmployeeService.GetAllEmployees(employeeSearchRequest);
            IEnumerable<Models.Employee> employeeList =
                employees.Employeess.Select(x => x.CreateFromServerToClientWithImage()).ToList();
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                aaData = employeeList,
                iTotalRecords = Convert.ToInt32(employees.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(employeeList.Count()),
                sEcho = employeeSearchRequest.sEcho,
            };
            if (userRole != null)
            {
                employeeViewModel.Role = userRole.Name;
            }
            return Json(employeeViewModel, JsonRequestBehavior.AllowGet);
        }
        [SiteAuthorize(PermissionKey = "PayrollDetail")]
        public ActionResult Detail(long? id, DateTime? date)
        {
            PayrollViewModel viewModel = new PayrollViewModel();
            if (id != null)
            {
                // get Employee
                if (date == null)
                {
                    date = DateTime.Now;
                }
                date = DateTime.Parse(date.ToString());
                PayrollResponse response = EmployeeService.FindEmployeeForPayroll(id, (DateTime)date);
                if (response.Employee != null)
                {
                    viewModel.Employee = response.Employee.CreateFromServerToClient();
                    viewModel.Id = viewModel.Employee.EmployeeId;
                }
                if (response.Allowance != null)
                {
                    viewModel.Allowances = response.Allowance.CreateFromServerToClient();
                }

                // get employee requests
                if (response.Requests != null)
                {
                    var requests = response.Requests.Select(x => x.CreateFromServerToClientPayroll()).Select(x => x.RequestDetails).ToList();
                    if (requests.Any())
                    {
                        switch (requests.Count)
                        {
                            case 2:
                                var deduction1 = requests[0].Select(x => x.InstallmentAmount).ToList();
                                if (deduction1.Any())
                                {
                                    if (deduction1[0] != null)
                                    {
                                        viewModel.Deduction1 = deduction1[0] ?? 0;
                                    }
                                }
                                var deduction2 = requests[1].Select(x => x.InstallmentAmount).ToList();
                                if (deduction2.Any())
                                {
                                    if (deduction2[0] != null)
                                    {
                                        viewModel.Deduction2 = deduction2[0] ?? 0;
                                    }
                                }
                                break;
                            case 1:
                                var deduction = requests[0].Select(x => x.InstallmentAmount).ToList();
                                if (deduction.Any())
                                {
                                    if (deduction[0] != null)
                                    {
                                        viewModel.Deduction1 = deduction[0] ?? 0;
                                    }
                                }
                                break;
                            case 0:
                                viewModel.Deduction1 = 0;
                                viewModel.Deduction2 = 0;
                                break;
                        }
                    }
                }
                double basicSalary = 0;
                double allowances = 0;
                if (viewModel.Employee.JobTitle != null)
                {
                    basicSalary = viewModel.Employee.JobTitle.BasicSalary;
                }
                if (viewModel.Allowances != null)
                {
                    allowances = viewModel.Allowances.Allowance1 + viewModel.Allowances.Allowance2 +
                                 viewModel.Allowances.Allowance3 + viewModel.Allowances.Allowance4 +
                                 viewModel.Allowances.Allowance5 ?? 0;
                }
                viewModel.Total = (basicSalary + allowances) - (viewModel.Deduction1 + viewModel.Deduction2);
                viewModel.Date = DateTime.Now.ToString("MM/yyyy"); ;
            }
            return View(viewModel);
        }
        [HttpPost]
        public JsonResult Detail(PayrollViewModel viewModel)
        {
            long id = viewModel.Id;
            if (id > 0)
            {
                var date = DateTime.ParseExact(viewModel.Date, "MM/yyyy", new CultureInfo("en"));
                PayrollResponse response = EmployeeService.FindEmployeeForPayroll(id,date);
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
                    var requests = response.Requests.Select(x => x.CreateFromServerToClientPayroll()).Select(x => x.RequestDetails).ToList();
                    if (requests.Any())
                    {
                        switch (requests.Count)
                        {
                            case 2:
                                var deduction1 = requests[0].Select(x => x.InstallmentAmount).ToList();
                                if (deduction1.Any())
                                {
                                    if (deduction1[0] != null)
                                    {
                                        viewModel.Deduction1 = deduction1[0] ?? 0;
                                    }
                                }
                                var deduction2 = requests[1].Select(x => x.InstallmentAmount).ToList();
                                if (deduction2.Any())
                                {
                                    if (deduction2[0] != null)
                                    {
                                        viewModel.Deduction2 = deduction2[0] ?? 0;
                                    }
                                }
                                break;
                            case 1:
                                var deduction = requests[0].Select(x => x.InstallmentAmount).ToList();
                                if (deduction.Any())
                                {
                                    if (deduction[0] != null)
                                    {
                                        viewModel.Deduction1 = deduction[0] ?? 0;
                                    }
                                }
                                break;
                            case 0:
                                viewModel.Deduction1 = 0;
                                viewModel.Deduction2 = 0;
                                break;
                        }
                    }
                }
                double basicSalary = 0;
                double allowances = 0;
                if (viewModel.Employee.JobTitle != null)
                {
                    basicSalary = viewModel.Employee.JobTitle.BasicSalary;
                }
                if (viewModel.Allowances != null)
                {
                    allowances = viewModel.Allowances.Allowance1 + viewModel.Allowances.Allowance2 +
                                 viewModel.Allowances.Allowance3 + viewModel.Allowances.Allowance4 +
                                 viewModel.Allowances.Allowance5 ?? 0;
                }
                viewModel.Total = (basicSalary + allowances) - (viewModel.Deduction1 + viewModel.Deduction2);
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}