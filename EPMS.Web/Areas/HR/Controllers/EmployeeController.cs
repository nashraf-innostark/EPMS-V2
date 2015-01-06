using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Employee;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Employee = EPMS.Web.Models.Employee;
using EmployeeRequest = EPMS.Web.Models.Request;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController
    {
        #region Private
        private readonly IEmployeeService EmployeeService;
        private readonly IJobTitleService JobTitleService;
        private readonly IDepartmentService DepartmentService;
        private readonly IAllowanceService AllowanceService;
        private readonly IAspNetUserService AspNetUserService;
        private readonly IEmployeeRequestService EmployeeRequestService;
        #endregion

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
                
                EmployeeViewModel employeeViewModel = new EmployeeViewModel
                {
                    DepartmentList = DepartmentService.GetAll(),
                    JobTitleList = JobTitleService.GetJobTitlesByDepartmentId(0),
                    SearchRequest = employeeSearchRequest,
                };
                employeeViewModel.Role = userRole.Name;
                return View(employeeViewModel);
            }
            if (userRole != null && userRole.Name == "Employee")
            {
                long id = AspNetUserService.FindById(User.Identity.GetUserId()).Employee.EmployeeId;
                return RedirectToAction("Create", new { id });
            }
            return null;
        }

        /// <summary>
        /// Get All Employees and return to View
        /// </summary>
        /// <param name="employeeSearchRequest">Employee Search Requset</param>
        /// <param name="param"></param>
        /// <returns>IEnumerable of All Employee</returns>
        [HttpPost]
        public ActionResult Index(EmployeeSearchRequset searchRequest)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            searchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
            searchRequest.SearchString = Request["search"];
            var employees = EmployeeService.GetAllEmployees(searchRequest);
            IEnumerable<Employee> employeeList =
                employees.Employeess.Select(x => x.CreateFromServerToClientWithImage()).ToList();
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                aaData = employeeList,
                iTotalRecords = Convert.ToInt32(employees.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(employees.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            if (userRole != null)
            {
                employeeViewModel.Role = userRole.Name;
            }

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
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            if (id == null)
            {
                EmployeeDetailViewModel viewModel = new EmployeeDetailViewModel
                {
                    EmployeeViewModel = { JobTitleList = JobTitleService.GetAll() }
                };
                viewModel.EmployeeViewModel.JobTitleDeptList = viewModel.EmployeeViewModel.JobTitleList.Select(x => x.CreateFromServerToClient());
                if (userRole != null) viewModel.Role = userRole.Name;
                viewModel.EmployeeViewModel.EmployeeName = "Add New Employee";
                viewModel.EmployeeViewModel.BtnText = "Save Employee";
                viewModel.EmployeeViewModel.PageTitle = "Employee Addition";
                return View(viewModel);
            }
            long empId = AspNetUserService.FindById(User.Identity.GetUserId()).Employee.EmployeeId;
            if (id > 0 && (id == empId || (userRole != null && userRole.Name == "Admin")))
            {
                EmployeeDetailViewModel viewModel = new EmployeeDetailViewModel
                {
                    EmployeeViewModel = { JobTitleList = JobTitleService.GetAll() }
                };
                viewModel.EmployeeViewModel.JobTitleDeptList = viewModel.EmployeeViewModel.JobTitleList.Select(x => x.CreateFromServerToClient());
                
                if (userRole != null && id > 0)
                {
                    viewModel.Role = userRole.Name;
                    // Get Employee
                    viewModel.EmployeeViewModel.Employee = EmployeeService.FindEmployeeById(id).CreateFromServerToClient();
                    // Set Employee Name for Header
                    viewModel.EmployeeViewModel.EmployeeName = viewModel.EmployeeViewModel.Employee.EmployeeNameE;
                    if (String.IsNullOrEmpty(viewModel.EmployeeViewModel.Employee.EmployeeImagePath))
                    {
                        viewModel.EmployeeViewModel.ImagePath = ConfigurationManager.AppSettings["EmployeeImage"] +
                                                                "profile.jpg";
                    }
                    else
                    {
                        viewModel.EmployeeViewModel.ImagePath = ConfigurationManager.AppSettings["EmployeeImage"] +
                                              viewModel.EmployeeViewModel.Employee.EmployeeImagePath;
                    }
                    if (userRole.Name == "Admin")
                    {
                        viewModel.EmployeeViewModel.PageTitle = "Employee's List";
                        viewModel.EmployeeViewModel.BtnText = "Update Changes";
                    }
                    if (userRole.Name == "Employee" || userRole.Name == "PM")
                    {
                        viewModel.EmployeeViewModel.PageTitle = "My Profile";
                        viewModel.EmployeeViewModel.BtnText = "Update Changes";
                    }
                    // get Employee requests
                    var empRequests = EmployeeRequestService.LoadAllMonetaryRequests(DateTime.Now, (long)id);
                    var requests = empRequests.Select(x => x.CreateFromServerToClientPayroll());
                    // get Employee request details
                    foreach (var reqDetail in requests)
                    {
                        var firstOrDefault = reqDetail.RequestDetails.FirstOrDefault();
                        if (firstOrDefault != null)
                            viewModel.EmployeeViewModel.Deduction1 = Math.Ceiling(firstOrDefault.InstallmentAmount ?? 0);
                        var lastOrDefault = reqDetail.RequestDetails.LastOrDefault();
                        if (lastOrDefault != null)
                            viewModel.EmployeeViewModel.Deduction2 = Math.Ceiling(lastOrDefault.InstallmentAmount ?? 0);
                    }
                    var employeeRequestResponse = EmployeeRequestService.LoadAllRequestsForEmployee(viewModel.EmployeeViewModel.Employee.EmployeeId);
                    var data = employeeRequestResponse.Select(x => x.CreateFromServerToClient());
                    var employeeRequests = data as IList<EmployeeRequest> ?? data.ToList();
                    if (employeeRequests.Any())
                    {
                        viewModel.RequestListViewModel.aaData = employeeRequests;
                    }
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
        public ActionResult Create(EmployeeDetailViewModel viewModel)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            try
            {
                if (userRole != null && userRole.Name == "Admin" && ModelState.IsValid)
                {
                    #region Update

                    viewModel.Role = userRole.Name;
                    if (viewModel.EmployeeViewModel.Employee.EmployeeId > 0)
                    {
                        // Set Employee Values
                        viewModel.EmployeeViewModel.Employee.RecLastUpdatedDt = DateTime.Now;
                        viewModel.EmployeeViewModel.Employee.RecLastUpdatedBy = User.Identity.Name;
                        // Set Values for Allownace
                        viewModel.EmployeeViewModel.Allowance.EmployeeId = viewModel.EmployeeViewModel.Employee.EmployeeId;
                        viewModel.EmployeeViewModel.Allowance.RecLastUpdatedBy = User.Identity.Name;
                        viewModel.EmployeeViewModel.Allowance.RecLastUpdatedDt = DateTime.Now;
                        // Update Employee
                        var employeeToUpdate = viewModel.EmployeeViewModel.Employee.CreateFromClientToServer();
                        // Update Allowance
                        var allowanceToTpdate = viewModel.EmployeeViewModel.Allowance.CreateFromClientToServer();
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
                        viewModel.EmployeeViewModel.Employee.RecCreatedDt = DateTime.Now;
                        viewModel.EmployeeViewModel.Employee.RecCreatedBy = User.Identity.Name;
                        string employeeJobId = GetEmployeeJobId();
                        viewModel.EmployeeViewModel.Employee.EmployeeJobId = employeeJobId;
                        // Add Employee
                        var employeeToSave = viewModel.EmployeeViewModel.Employee.CreateFromClientToServer();
                        long employeeId = EmployeeService.AddEmployee(employeeToSave);

                        // Set Values for Allownace
                        viewModel.EmployeeViewModel.Allowance.EmployeeId = employeeId;
                        viewModel.EmployeeViewModel.Allowance.RecLastUpdatedBy = User.Identity.Name;
                        viewModel.EmployeeViewModel.Allowance.RecLastUpdatedDt = DateTime.Now;
                        // Add Allowance
                        var allowanceToSave = viewModel.EmployeeViewModel.Allowance.CreateFromClientToServer();
                        if (AllowanceService.AddAllowance(allowanceToSave) && employeeId > 0)
                        {
                            TempData["message"] = new MessageViewModel
                            {
                                Message = "Employee has been Added",
                                IsSaved = true
                            };
                            viewModel.EmployeeViewModel.Employee.EmployeeId = employeeToSave.EmployeeId;
                            return RedirectToAction("Index");
                        }
                    }

                    #endregion
                }
                else if (userRole != null && (userRole.Name == "Employee" || userRole.Name == "PM"))
                {
                    viewModel.Role = userRole.Name;
                }
            }
            catch (Exception)
            {
                TempData["message"] = new MessageViewModel { Message = "Problem in Saving Employee", IsError = true };
            }
            viewModel.EmployeeViewModel.JobTitleList = JobTitleService.GetAll();
            viewModel.EmployeeViewModel.JobTitleDeptList = viewModel.EmployeeViewModel.JobTitleList.Select(x => x.CreateFromServerToClient());
            TempData["message"] = new MessageViewModel { Message = "Problem in Saving Employee", IsError = true };
            return View(viewModel);
        }
        #endregion

        #region Employee Job ID

        string GetEmployeeJobId()
        {
            string year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            var result = EmployeeService.GetAll();
            var userRole = result.LastOrDefault();
            if (userRole != null)
            {
                string jId = userRole.EmployeeJobId;
                jId = jId.Substring(Math.Max(0, jId.Length - 4));
                int id = Convert.ToInt32(jId) + 1;
                int len = id.ToString().Length;
                string zeros = "";
                switch (len)
                {
                    case 1:
                        zeros = "000";
                        break;
                    case 2:
                        zeros = "00";
                        break;
                    case 3:
                        zeros = "0";
                        break;
                    case 4:
                        zeros = "";
                        break;
                }
                string jobId = year + zeros + id.ToString(CultureInfo.InvariantCulture);
                return jobId;
            }
            return String.Empty;
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

        #region Get Employee Requests

        public ActionResult GetEmployeeRequests(JQueryDataTableParamModel param, EmployeeDetailViewModel viewModel)
        {
            //IEnumerable<EmployeeRequest> aaData;
            //int iTotalRecords;
            //int iTotalDisplayRecords;
            //string sEcho;
            //try
            //{
            //    AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            //    var userRole = result.AspNetRoles.FirstOrDefault();
            //    if (userRole != null && userRole.Name == "Admin")
            //    {
            //        viewModel.RequestListViewModel.SearchRequest.Requester = "Admin";
            //    }
            //    else
            //    {
            //        viewModel.RequestListViewModel.SearchRequest.Requester = AspNetUserService.FindById(User.Identity.GetUserId()).EmployeeId.ToString();
            //    }
            //    viewModel.RequestListViewModel.SearchRequest.SearchStr = Request["reqSearch"];
            //    var employeeRequestResponse = EmployeeRequestService.LoadAllRequests(viewModel.EmployeeRequestViewModel.SearchRequest);
            //    var data = employeeRequestResponse.EmployeeRequests.Select(x => x.CreateFromServerToClient());
            //    var employeeRequests = data as IList<EmployeeRequest> ?? data.ToList();
                
            //    ////long empId = AspNetUserService.FindById(User.Identity.GetUserId()).Employee.EmployeeId;
            //    ////var employeeRequest = EmployeeRequestService.LoadAllRequestsForEmployee(empId);
            //    ////var employeeRequests = employeeRequest as IList<EPMS.Models.DomainModels.EmployeeRequest> ?? employeeRequest.ToList();
            //    ////var data = employeeRequests.Select(x => x.CreateFromServerToClient());
            //    ////var enumerable = data as IList<EmployeeRequest> ?? data.ToList();
            //    //if (enumerable.Any())
            //    if (employeeRequests.Any())
            //    {
            //        aaData = employeeRequests;
            //        iTotalRecords = employeeRequests.Count();
            //        iTotalDisplayRecords = employeeRequests.Count();
            //        sEcho = param.sEcho;
            //    }
            //    else
            //    {
            //        aaData = Enumerable.Empty<EmployeeRequest>();
            //        iTotalRecords = 0;
            //        iTotalDisplayRecords = 0;
            //        sEcho = param.sEcho;
            //    }
            //}
            //catch (Exception exp)
            //{
            //    return Json(new { response = "Failed to load.", status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            //}
            //return Json(new { aaData, iTotalRecords, iTotalDisplayRecords, sEcho, response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
            return Json(new { response = "Failed to load.", status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
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