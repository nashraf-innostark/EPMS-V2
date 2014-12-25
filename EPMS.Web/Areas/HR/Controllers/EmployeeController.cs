﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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
using EPMS.Web.ViewModels.Employee;
using EPMS.Models.ResponseModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Employee = EPMS.Web.Models.Employee;
using EmployeeRequest = EPMS.Web.Models.EmployeeRequest;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService EmployeeService;
        private readonly IJobTitleService JobTitleService;
        private readonly IDepartmentService DepartmentService;
        private readonly IAllowanceService AllowanceService;
        private readonly IAspNetUserService AspNetUserService;
        private readonly IEmployeeRequestService EmployeeRequestService;

        #region Constructor

        public EmployeeController(IEmployeeService employeeService, IEmployeeRequestService employeeRequestService, IDepartmentService departmentService, IJobTitleService jobTitleService, IAllowanceService allowanceService, IAspNetUserService aspNetUserService)
        {
            EmployeeService = employeeService;
            DepartmentService = departmentService;
            JobTitleService = jobTitleService;
            AllowanceService = allowanceService;
            AspNetUserService = aspNetUserService;
            EmployeeRequestService = employeeRequestService;
        }

        #endregion

        #region Public

        #region Employee List View
        /// <summary>
        /// Employee ListView Action Method
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            if (userRole != null && (userRole.Name == "Admin" || userRole.Name == "PM"))
            {
                EmployeeSearchRequset employeeSearchRequest = new EmployeeSearchRequset();
                ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;

                EmployeeViewModel employeeViewModel = new EmployeeViewModel
                {
                    DepartmentList = DepartmentService.GetAll(),
                    JobTitleList = JobTitleService.GetJobTitlesByDepartmentId(0),
                    SearchRequest = employeeSearchRequest,
                };
                return View(employeeViewModel);
            }
            if (Roles.IsUserInRole("Employee"))
            {
                long id = AspNetUserService.FindById(User.Identity.GetUserId()).Employee.EmployeeId;
                return RedirectToAction("Create", new { id = id });
            }
            return null;
        }
        /// <summary>
        /// Get All Employees and return to View
        /// </summary>
        /// <param name="employeeSearchRequest">Employee Search Requset</param>
        /// <returns>IEnumerable of All Employee</returns>
        [HttpPost]
        public ActionResult Index(EmployeeSearchRequset employeeSearchRequest)
        {
            employeeSearchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
            var employees = EmployeeService.GetAllEmployees(employeeSearchRequest);
            IEnumerable<Employee> employeeList =
                employees.Employeess.Select(x => x.CreateFromServerToClientWithImage()).ToList();
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                aaData = employeeList,
                iTotalRecords = employees.TotalCount,
                iTotalDisplayRecords = employeeList.Count(),
                sEcho = employeeList.Count(),
            };

            return Json(employeeViewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get Job Titles
        /// <summary>
        /// Get all job titles from DB
        /// </summary>
        /// <param name="deptId">Department ID</param>
        /// <returns>List of JobTitle in JsonResult fromat</returns>
        [HttpGet]
        public JsonResult GetJobTitles(long deptId)
        {
            var jobTitles = JobTitleService.GetJobTitlesByDepartmentId(deptId).Select(j => j.CreateForDropDown());
            return Json(jobTitles, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add/Update Employee

        /// <summary>
        /// Add or Update Employee Action Method
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns></returns>
        public ActionResult Create(long? id)
        {
            if (id == null)
            {
                EmployeeDetailViewModel viewModel = new EmployeeDetailViewModel
                {
                    EmployeeViewModel = {JobTitleList = JobTitleService.GetAll()}
                };
                viewModel.EmployeeViewModel.JobTitleDeptList = viewModel.EmployeeViewModel.JobTitleList.Select(x => x.CreateFromServerToClient());
                viewModel.EmployeeViewModel.EmployeeName = "Add New Employee";
                viewModel.EmployeeViewModel.BtnText = "Save Employee";
                viewModel.EmployeeViewModel.PageTitle = "Employee Addition";
                return View(viewModel);
            }
            long empId = AspNetUserService.FindById(User.Identity.GetUserId()).Employee.EmployeeId;
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            if (id > 0 && (id == empId || (userRole !=null && userRole.Name == "Admin")))
            {
                EmployeeDetailViewModel viewModel = new EmployeeDetailViewModel
                {
                    EmployeeViewModel = { JobTitleList = JobTitleService.GetAll() }
                };
                viewModel.EmployeeViewModel.JobTitleDeptList = viewModel.EmployeeViewModel.JobTitleList.Select(x => x.CreateFromServerToClient());
                // Get User Role
                
                if (userRole != null)
                {
                    if (id > 0 && userRole.Name == "Admin")
                    {
                        viewModel.EmployeeViewModel.Employee = EmployeeService.FindEmployeeById(id).CreateFromServerToClient();
                        viewModel.EmployeeViewModel.EmployeeName = viewModel.EmployeeViewModel.Employee.EmployeeNameE;
                        viewModel.EmployeeViewModel.PageTitle = "Employee's List";
                        viewModel.EmployeeViewModel.BtnText = "Update Changes";
                        viewModel.EmployeeViewModel.ImagePath = ConfigurationManager.AppSettings["EmployeeImage"] +
                                              viewModel.EmployeeViewModel.Employee.EmployeeImagePath;
                    }
                    if (id > 0 && (userRole.Name == "Employee" || userRole.Name == "PM"))
                    {
                        viewModel.EmployeeViewModel.Employee = EmployeeService.FindEmployeeById(id).CreateFromServerToClient();
                        viewModel.EmployeeViewModel.EmployeeName = viewModel.EmployeeViewModel.Employee.EmployeeNameE;
                        viewModel.EmployeeViewModel.PageTitle = "My Profile";
                        viewModel.EmployeeViewModel.BtnText = "Update Changes";
                        viewModel.EmployeeViewModel.ImagePath = ConfigurationManager.AppSettings["EmployeeImage"] +
                                              viewModel.EmployeeViewModel.Employee.EmployeeImagePath;
                    }
                    //if (userRole.Name == "Employee" || userRole.Name == "PM")
                    //{
                    //    var employeeRequestResponse = EmployeeRequestService.LoadAllRequests(new EmployeeRequestSearchRequest());
                    //    var data = employeeRequestResponse.EmployeeRequests.Select(x => x.CreateFromServerToClient());
                    //    var employeeRequests = data as IList<EmployeeRequest> ?? data.ToList();
                    //    if (employeeRequests.Any())
                    //    {
                    //        viewModel.aaData1 = employeeRequests;
                    //        viewModel.iTotalRecords = employeeRequestResponse.TotalCount;
                    //        viewModel.iTotalDisplayRecords = employeeRequestResponse.EmployeeRequests.Count();
                    //        viewModel.sEcho = employeeRequestResponse.EmployeeRequests.Count();
                    //    }
                    //    else
                    //    {
                    //        viewModel.aaData1 = Enumerable.Empty<EmployeeRequest>();
                    //        viewModel.iTotalRecords = employeeRequestResponse.TotalCount;
                    //        viewModel.iTotalDisplayRecords = employeeRequestResponse.EmployeeRequests.Count();
                    //        viewModel.sEcho = 1;
                    //    }
                    //}
                }
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        /// <summary>
        /// Add or Update Employee record
        /// </summary>
        /// <param name="viewModel">Employee View Model</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Create(EmployeeViewModel viewModel)
        {
            try
            {
                if (Roles.IsUserInRole("Admin"))
                {
                    #region Update

                    if (viewModel.Employee.EmployeeId > 0)
                    {
                        // Set Employee Values
                        viewModel.Employee.RecLastUpdatedDt = DateTime.Now;
                        viewModel.Employee.RecLastUpdatedBy = User.Identity.Name;
                        // Set Values for Allownace
                        viewModel.Allowance.EmployeeId = viewModel.Employee.EmployeeId;
                        viewModel.Allowance.RecLastUpdatedBy = User.Identity.Name;
                        viewModel.Allowance.RecLastUpdatedDt = DateTime.Now;
                        // Update Employee and Allowance
                        var employeeToUpdate = viewModel.Employee.CreateFromClientToServer();
                        var allowanceToTpdate = viewModel.Allowance.CreateFromClientToServer();
                        if (EmployeeService.UpdateEmployee(employeeToUpdate) &&
                            AllowanceService.UpdateAllowance(allowanceToTpdate))
                        {
                            TempData["message"] = new MessageViewModel
                            {
                                Message = "Employee has been Updated",
                                IsUpdated = true
                            };
                            return RedirectToAction("Index");
                        }
                    }

                    #endregion

                    #region Add

                    else
                    {
                        // Set Employee Values
                        viewModel.Employee.RecCreatedDt = DateTime.Now;
                        viewModel.Employee.RecCreatedBy = User.Identity.Name;
                        var employeeToSave = viewModel.Employee.CreateFromClientToServer();
                        long employeeId = EmployeeService.AddEmployee(employeeToSave);

                        // Set Values for Allownace
                        viewModel.Allowance.EmployeeId = employeeId;
                        viewModel.Allowance.RecLastUpdatedBy = User.Identity.Name;
                        viewModel.Allowance.RecLastUpdatedDt = DateTime.Now;

                        var allowanceToSave = viewModel.Allowance.CreateFromClientToServer();

                        if (AllowanceService.AddAllowance(allowanceToSave) && employeeId > 0)
                        {
                            TempData["message"] = new MessageViewModel
                            {
                                Message = "Employee has been Added",
                                IsSaved = true
                            };
                            viewModel.Employee.EmployeeId = employeeToSave.EmployeeId;
                            return RedirectToAction("Index");
                        }
                    }

                    #endregion
                }
                else if (Roles.IsUserInRole("Employee") || Roles.IsUserInRole("PM"))
                {

                }
            }
            catch (Exception)
            {
                TempData["message"] = new MessageViewModel { Message = "Problem in Saving Employee", IsError = true };
            }
            viewModel.JobTitleList = JobTitleService.GetAll();
            viewModel.JobTitleDeptList = viewModel.JobTitleList.Select(x => x.CreateFromServerToClient());
            TempData["message"] = new MessageViewModel { Message = "Problem in Saving Employee", IsError = true };
            return View(viewModel);
        }
        #endregion

        #region Upload Employee Photo
        /// <summary>
        /// Upload Employee Photo
        /// </summary>
        /// <returns></returns>

        public ActionResult UploadEmployeePhoto()
        {
            string imagename = "";
            HttpPostedFileBase userPhoto = Request.Files[0];
            try
            {
                //Save image to Folder
                if ((userPhoto != null))
                {
                    imagename = (DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(".", "") + userPhoto.FileName).Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["EmployeeImage"]);
                    string savedFileName = Path.Combine(filePathOriginal, imagename);
                    userPhoto.SaveAs(savedFileName);
                }
            }
            catch (Exception exp)
            {
                return Json(new { response = "Failed to upload. Error: " + exp.Message, status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { filename = imagename, size = userPhoto.ContentLength / 1024 + "KB", response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete Employee
        /// <summary>
        /// Delete Employee Data from DB
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Json for toast message</returns>
        public ActionResult Delete(int employeeId)
        {
            var employeeToBeDeleted = EmployeeService.FindEmployeeById(employeeId);
            try
            {
                EmployeeService.DeleteEmployee(employeeToBeDeleted);
                return Json(new
                {
                    response = "Employee has been deleted",
                    status = (int)HttpStatusCode.OK
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exp)
            {
                return
                    Json(
                        new
                        {
                            response = "Failed to delete employee. Error: " + exp.Message,
                            status = (int)HttpStatusCode.BadRequest
                        }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #endregion
    }
}