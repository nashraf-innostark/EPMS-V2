﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Employee;
using Microsoft.AspNet.Identity;
using EPMS.Web.Models;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService EmployeeService;
        private readonly IJobTitleService JobTitleService;
        private readonly IDepartmentService DepartmentService;
        private readonly IAllowanceService AllowanceService;

        #region Constructor

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IJobTitleService jobTitleService, IAllowanceService allowanceService)
        {
            EmployeeService = employeeService;
            DepartmentService = departmentService;
            JobTitleService = jobTitleService;
            AllowanceService = allowanceService;
        }

        #endregion

        #region Employee List View
        /// <summary>
        /// Employee ListView Action Method
        /// </summary>
        /// <returns></returns>
        public ActionResult Employees()
        {
            EmployeeSearchRequset employeeSearchRequest = Session["PageMetaData"] as EmployeeSearchRequset;

            Session["PageMetaData"] = null;

            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;

            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                DepartmentList = DepartmentService.GetAll(),
                JobTitleList = JobTitleService.GetJobTitlesByDepartmentId(0),
                SearchRequest = employeeSearchRequest ?? new EmployeeSearchRequset()
            };
            return View(employeeViewModel);
        }
        /// <summary>
        /// Get All Employees and return to View
        /// </summary>
        /// <param name="employeeSearchRequest">Employee Search Requset</param>
        /// <returns>IEnumerable of All Employee</returns>
        [HttpPost]
        public ActionResult Employees(EmployeeSearchRequset employeeSearchRequest)
        {
            //string rawSearch = HttpContext.Current.Request.Params["sSearch"];
            employeeSearchRequest.UserId = Guid.Parse(User.Identity.GetUserId());//Guid.Parse(Session["LoginID"] as string);
            var employees = EmployeeService.GetAllEmployees(employeeSearchRequest);
            IEnumerable<Employee> employeeList =
                employees.Employeess.Select(x => x.CreateFromWithImage()).ToList();
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                FilePath = (ConfigurationManager.AppSettings["EmployeeImage"] + User.Identity.Name + "/"),
                aaData = employeeList,
                iTotalRecords = employees.TotalCount,
                iTotalDisplayRecords = employeeList.Count(),
                sEcho = employeeList.Count(),
            };

            // Keep Search Request in Session
            Session["PageMetaData"] = employeeSearchRequest;

            return Json(employeeViewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get JobTitle
        /// <summary>
        /// Get all job titles from DB
        /// </summary>
        /// <param name="deptId">Department ID</param>
        /// <returns>List of JobTitle in JsonResult fromat</returns>
        [HttpGet]
        public JsonResult GetJobTitles(long deptId)
        {
            var jobTitles = JobTitleService.GetJobTitlesByDepartmentId(deptId).Select(j => j.CreateFromDropDown());
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
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                JobTitleList = JobTitleService.GetAll()
            };
            viewModel.JobTitleDeptList = viewModel.JobTitleList.Select(x => x.CreateFromJob());
            if (id != null)
            {
                viewModel.Employee = EmployeeService.FindEmployeeById(id).CreateFrom();
            }
            return View(viewModel);
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
                    var employeeToUpdate = viewModel.Employee.CreateFrom();
                    var allowanceToTpdate = viewModel.Allowance.CreateFrom();
                    if (EmployeeService.UpdateEmployee(employeeToUpdate) && AllowanceService.UpdateAllowance(allowanceToTpdate))
                    {
                        TempData["message"] = new MessageViewModel { Message = "Employee has been Updated", IsUpdated = true };
                        return RedirectToAction("Employees");
                    }
                }
                #endregion

                #region Add

                else
                {
                    // Set Employee Values
                    viewModel.Employee.RecCreatedDt = DateTime.Now;
                    viewModel.Employee.RecCreatedBy = User.Identity.Name;
                    var employeeToSave = viewModel.Employee.CreateFrom();
                    long employeeId = EmployeeService.AddEmployee(employeeToSave);

                    // Set Values for Allownace
                    viewModel.Allowance.EmployeeId = employeeId;
                    viewModel.Allowance.RecLastUpdatedBy = User.Identity.Name;
                    viewModel.Allowance.RecLastUpdatedDt = DateTime.Now;

                    var allowanceToSave = viewModel.Allowance.CreateFrom();

                    if (AllowanceService.AddAllowance(allowanceToSave) && employeeId > 0)
                    {
                        TempData["message"] = new MessageViewModel { Message = "Employee has been Added", IsSaved = true };
                        viewModel.Employee.EmployeeId = employeeToSave.EmployeeId;
                        return RedirectToAction("Employees");
                    }
                }

                #endregion
            }
            catch (Exception)
            {
                TempData["message"] = new MessageViewModel { Message = "Problem in Saving Employee", IsError = true };
            }
            viewModel.JobTitleList = JobTitleService.GetAll();
            viewModel.JobTitleDeptList = viewModel.JobTitleList.Select(x => x.CreateFromJob());
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

        #region Employee Detail Page
        /// <summary>
        /// Show Details of Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View</returns>
        public ActionResult Details(long? id)
        {
            EmployeeDetailViewModel viewModel = new EmployeeDetailViewModel
            {
                JobTitleList = JobTitleService.GetAll(),
            };
            viewModel.JobTitleDeptList = viewModel.JobTitleList.Select(x => x.CreateFromJob());
            if (id != null)
            {
                viewModel.Employee = EmployeeService.FindEmployeeById(id).CreateFrom();
            }
            return View(viewModel);
        }
        #endregion

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
    }
}