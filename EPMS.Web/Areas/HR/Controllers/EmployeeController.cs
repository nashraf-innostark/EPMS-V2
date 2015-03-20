using System;
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
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.PMS;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Employee;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Allowance = EPMS.Web.Models.Allowance;
using Employee = EPMS.Web.Models.Employee;
using EmployeeRequest = EPMS.Web.Models.Request;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "HRS", IsModule = true)]
    public class EmployeeController : BaseController
    {
        #region Private
        private readonly IEmployeeService EmployeeService;
        private readonly IJobTitleService JobTitleService;
        private readonly IDepartmentService DepartmentService;
        private readonly IAllowanceService AllowanceService;
        private readonly IAspNetUserService AspNetUserService;
        private readonly IEmployeeRequestService EmployeeRequestService;
        private readonly ITaskEmployeeService TaskEmployeeService;
        #endregion

        #region Constructor

        public EmployeeController(IEmployeeService employeeService, IEmployeeRequestService employeeRequestService, IDepartmentService departmentService, IJobTitleService jobTitleService, IAllowanceService allowanceService, IAspNetUserService aspNetUserService, ITaskEmployeeService taskEmployeeService)
        {
            EmployeeService = employeeService;
            DepartmentService = departmentService;
            JobTitleService = jobTitleService;
            AllowanceService = allowanceService;
            AspNetUserService = aspNetUserService;
            TaskEmployeeService = taskEmployeeService;
            EmployeeRequestService = employeeRequestService;
        }

        #endregion

        #region Public

        #region Employee List View
        /// <summary>
        /// Employee ListView Action Method
        /// </summary>
        /// <returns></returns>
        [SiteAuthorize(PermissionKey = "EmployeeIndex")]
        public ActionResult Index()
        {
            if (Convert.ToString(Session["RoleName"]) == "Employee")
            {
                long id = AspNetUserService.FindById(User.Identity.GetUserId()).Employee.EmployeeId;
                return RedirectToAction("Create", new { id });
            }
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                SearchRequest = new EmployeeSearchRequset()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(employeeViewModel);
        }

        /// <summary>
        /// Get All Employees and return to View
        /// </summary>
        /// <param name="searchRequest">Employee Search Requset</param>
        /// <returns>IEnumerable of All Employee</returns>
        [HttpPost]
        public ActionResult Index(EmployeeSearchRequset searchRequest)
        {
            searchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
            searchRequest.SearchString = Request["search"];
            EmployeeResponse employees = EmployeeService.GetAllEmployees(searchRequest);
            IEnumerable<Employee> employeeList =
                employees.Employeess.Select(x => x.CreateFromServerToClientWithImage()).ToList();
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                aaData = employeeList,
                iTotalRecords = Convert.ToInt32(employees.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(employees.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
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
        [SiteAuthorize(PermissionKey = "EmployeeCreate,EmployeeDetails")]
        public ActionResult Create(long? id)
        {
            var direction = Resources.Shared.Common.TextDirection;
            if (id == null)
            {
                EmployeeDetailViewModel viewModel = new EmployeeDetailViewModel
                {
                    EmployeeViewModel =
                    {
                        JobTitleList = JobTitleService.GetAll()
                    }
                };
                string employeeJobId = GetEmployeeJobId();
                viewModel.EmployeeViewModel.Employee.EmployeeJobId = employeeJobId;
                viewModel.EmployeeViewModel.JobTitleDeptList = viewModel.EmployeeViewModel.JobTitleList.Select(x => x.CreateFromServerToClient());
                viewModel.EmployeeViewModel.EmployeeName = Resources.HR.Employee.AddNew;
                viewModel.EmployeeViewModel.BtnText = Resources.HR.Employee.BtnSave;
                viewModel.EmployeeViewModel.PageTitle = Resources.HR.Employee.PTAdd;
                return View(viewModel);
            }
            if (id > 0)
            {
                EmployeeDetailViewModel viewModel = new EmployeeDetailViewModel();
                // Get Employee Along Job History
                EmployeeResponse employeeResponse = EmployeeService.GetEmployeeAlongJobHistory(id);
                viewModel.EmployeeViewModel.Employee = employeeResponse.Employee.CreateFromServerToClient();
                viewModel.EmployeeViewModel.JobTitleDeptList = employeeResponse.JobTitleList.Select(x => x.CreateFromServerToClient());
                // Get Allowances
                var allowance =
                    AllowanceService.FindByEmpIdDate(viewModel.EmployeeViewModel.Employee.EmployeeId, DateTime.Now);
                if (allowance != null)
                {
                    viewModel.EmployeeViewModel.Allowance = allowance.CreateFromServerToClient();
                    viewModel.EmployeeViewModel.OldAllowance = viewModel.EmployeeViewModel.Allowance;
                }
                viewModel.EmployeeViewModel.JobHistories = employeeResponse.JobHistories.Select(x=>x.CreateFromServerToClientForJobHistory()).ToList();
                viewModel.EmployeeViewModel.EmployeeName = direction == "ltr" ? viewModel.EmployeeViewModel.Employee.EmployeeFullNameE : viewModel.EmployeeViewModel.Employee.EmployeeFullNameA;
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
                if (Convert.ToString(Session["RoleName"]) == "Admin")
                {
                    viewModel.EmployeeViewModel.PageTitle = Resources.HR.Employee.PTList;
                    viewModel.EmployeeViewModel.BtnText = Resources.HR.Employee.BtnUpdate;
                }
                else
                {
                    viewModel.EmployeeViewModel.PageTitle = Resources.HR.Employee.PTProfile;
                    viewModel.EmployeeViewModel.BtnText = Resources.HR.Employee.BtnUpdate;
                }
                // get Employee requests
                var requests = employeeResponse.EmployeeMonetaryRequests.Select(x => x.CreateFromServerToClientEmpDetail()).ToList();
                if (requests.Count() == 2)
                {
                    var first = requests[0].RequestDetails.ToList();
                    if (first.Any())
                    {
                        var val1 = first[0].InstallmentAmount;
                        viewModel.EmployeeViewModel.Deduction1 = Convert.ToInt32(val1);
                    }
                    var last = requests[1].RequestDetails.ToList();
                    if (last.Any())
                    {
                        var val2 = last[0].InstallmentAmount;
                        viewModel.EmployeeViewModel.Deduction2 = Convert.ToInt32(val2);
                    }
                }
                else if (requests.Count() == 1)
                {
                    var first = requests[0].RequestDetails.ToList();
                    if (first.Any())
                    {
                        var val1 = first[0].InstallmentAmount;
                        viewModel.EmployeeViewModel.Deduction1 = Convert.ToInt32(val1);
                    }
                }
                var data = employeeResponse.EmployeeRequests.Select(x => x.CreateFromServerToClient());
                var employeeRequests = data as IList<EmployeeRequest> ?? data.ToList();
                if (employeeRequests.Any())
                {
                    viewModel.RequestListViewModel.aaData = employeeRequests;
                }
                var taskData = employeeResponse.EmployeeTasks.Select(x => x.CreateFromServerToClient());
                var employeeTasks = taskData as IList<TaskEmployee> ?? taskData.ToList();
                if (employeeTasks.Any())
                {
                    viewModel.TaskEmployees = employeeTasks;
                }

                return View(viewModel);
            }
            return null;
        }

        /// <summary>
        /// Add or Update Employee record
        /// </summary>
        /// <param name="viewModel">Employee View Model</param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(EmployeeDetailViewModel viewModel)
        {
            var direction = Resources.Shared.Common.TextDirection;
            try
            {
                if (Request.Form["Save"] != null)
                {
                    #region Update

                    viewModel.Role = Convert.ToString(Session["RoleName"]);
                    if (viewModel.EmployeeViewModel.Employee.EmployeeId > 0)
                    {
                        // Set Employee Values
                        viewModel.EmployeeViewModel.Employee.RecLastUpdatedDt = DateTime.Now;
                        viewModel.EmployeeViewModel.Employee.RecLastUpdatedBy = User.Identity.GetUserId();
                        // Update Employee
                        var employeeToUpdate = viewModel.EmployeeViewModel.Employee.CreateFromClientToServer();
                        // check if allowance has been updated
                        var isUpdated = CheckIfAllowanceUpdated(viewModel.EmployeeViewModel.Allowance,
                            viewModel.EmployeeViewModel.OldAllowance);
                        if (isUpdated)
                        {
                            // Add new Allowance
                            viewModel.EmployeeViewModel.Allowance.EmployeeId =
                                viewModel.EmployeeViewModel.Employee.EmployeeId;
                            viewModel.EmployeeViewModel.Allowance.AllowanceDate = DateTime.Now;
                            viewModel.EmployeeViewModel.Allowance.RecCreatedBy = User.Identity.GetUserId();
                            viewModel.EmployeeViewModel.Allowance.RecCreatedDt = DateTime.Now;
                            // save Allowance
                            var allowanceToAdd = viewModel.EmployeeViewModel.Allowance.CreateFromClientToServer();
                            AllowanceService.AddAllowance(allowanceToAdd);
                        }
                        if (EmployeeService.UpdateEmployee(employeeToUpdate))
                        {
                            TempData["message"] = new MessageViewModel
                            {
                                Message = Resources.HR.Employee.UpdateMessage,
                                IsUpdated = true
                            };
                            return RedirectToAction("Index");
                        }
                        viewModel.EmployeeViewModel.JobTitleList = JobTitleService.GetAll();
                        viewModel.EmployeeViewModel.JobTitleDeptList =
                            viewModel.EmployeeViewModel.JobTitleList.Select(x => x.CreateFromServerToClient());
                        viewModel.Role = Convert.ToString(Session["RoleName"]);
                        viewModel.EmployeeViewModel.EmployeeName = Resources.HR.Employee.AddNew;
                        viewModel.EmployeeViewModel.BtnText = Resources.HR.Employee.BtnSave;
                        viewModel.EmployeeViewModel.PageTitle = Resources.HR.Employee.PTAdd;
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.HR.Employee.ProblemSaving,
                            IsError = true
                        };
                        return View(viewModel);
                    }

                    #endregion

                    #region Add

                    // Set Employee Values
                    viewModel.EmployeeViewModel.Employee.EmployeeIqamaIssueDt = DateTime.Now.ToShortDateString();
                    viewModel.EmployeeViewModel.Employee.RecCreatedDt = DateTime.Now;
                    viewModel.EmployeeViewModel.Employee.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.EmployeeViewModel.Employee.IsActivated = true;
                    // Add Employee
                    var employeeToSave = viewModel.EmployeeViewModel.Employee.CreateFromClientToServer();
                    long employeeId = EmployeeService.AddEmployee(employeeToSave);

                    // Set Values for Allownace
                    viewModel.EmployeeViewModel.Allowance.EmployeeId = employeeId;
                    viewModel.EmployeeViewModel.Allowance.AllowanceDate = DateTime.Now;
                    viewModel.EmployeeViewModel.Allowance.RecLastUpdatedBy = User.Identity.GetUserId();
                    viewModel.EmployeeViewModel.Allowance.RecLastUpdatedDt = DateTime.Now;
                    // Add Allowance
                    var allowanceToSave = viewModel.EmployeeViewModel.Allowance.CreateFromClientToServer();
                    if (AllowanceService.AddAllowance(allowanceToSave) && employeeId > 0)
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.HR.Employee.AddMessage,
                            IsSaved = true
                        };
                        viewModel.EmployeeViewModel.Employee.EmployeeId = employeeToSave.EmployeeId;
                        return RedirectToAction("Index");
                    }

                    #endregion
                }
                if (Request.Form["Deactivate"] != null)
                {
                    viewModel.EmployeeViewModel.Employee.IsActivated = false;
                    var employeeToUpdate = viewModel.EmployeeViewModel.Employee.CreateFromClientToServer();
                    if (EmployeeService.UpdateEmployee(employeeToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.HR.Employee.DeactivateMessage,
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
                if (Request.Form["Activate"] != null)
                {
                    viewModel.EmployeeViewModel.Employee.IsActivated = true;
                    var employeeToUpdate = viewModel.EmployeeViewModel.Employee.CreateFromClientToServer();
                    if (EmployeeService.UpdateEmployee(employeeToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.HR.Employee.ActivateMessage,
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = Resources.HR.Employee.ProblemSaving,
                    IsError = true
                };
            }
            viewModel.EmployeeViewModel.JobTitleList = JobTitleService.GetAll();
            viewModel.EmployeeViewModel.JobTitleDeptList = viewModel.EmployeeViewModel.JobTitleList.Select(x => x.CreateFromServerToClient());
            viewModel.Role = Convert.ToString(Session["RoleName"]);
            viewModel.EmployeeViewModel.EmployeeName = Resources.HR.Employee.AddNew;
            viewModel.EmployeeViewModel.BtnText = Resources.HR.Employee.BtnSave;
            viewModel.EmployeeViewModel.PageTitle = Resources.HR.Employee.PTAdd;
            TempData["message"] = new MessageViewModel { Message = Resources.HR.Employee.ProblemSaving, IsError = true };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        #endregion

        #region CheckIfAllowanceUpdated

        bool CheckIfAllowanceUpdated(Allowance newAllowance, Allowance oldAllowance)
        {
            if (oldAllowance.Allowance1 != newAllowance.Allowance1 || oldAllowance.Allowance2 != newAllowance.Allowance2 ||
                oldAllowance.Allowance3 != newAllowance.Allowance3 || oldAllowance.Allowance4 != newAllowance.Allowance4 ||
                oldAllowance.Allowance5 != newAllowance.Allowance5)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Employee Job ID

        string GetEmployeeJobId()
        {
            string year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            var result = EmployeeService.GetAll();
            var employee = result.LastOrDefault();
            if (employee != null)
            {
                string jId = employee.EmployeeJobId;
                string eYear = jId.Substring(0, 4);
                if (eYear != year || !eYear.Contains(year))
                {
                    jId = "0";
                }
                else
                {
                    jId = jId.Substring(Math.Max(0, jId.Length - 4));
                }
                int id = Convert.ToInt32(jId) + 1;
                int len = id.ToString(CultureInfo.InvariantCulture).Length;
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
            return DateTime.Now.Year + "0001";
        }
        #endregion

        #region Upload Employee Photo
        /// <summary>
        /// Upload Employee Photo
        /// </summary>
        /// <returns></returns>

        public ActionResult UploadEmployeePhoto()
        {
            HttpPostedFileBase userPhoto = Request.Files[0];
            if ((userPhoto != null))
            {
                try
                {
                    //Save image to Folder
                    string imagename = (DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(".", "") + userPhoto.FileName)
                        .Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["EmployeeImage"]);
                    string savedFileName = Path.Combine(filePathOriginal, imagename);
                    userPhoto.SaveAs(savedFileName);
                    return Json(new { filename = imagename, size = userPhoto.ContentLength / 1024 + "KB", response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception exp)
                {
                    return
                        Json(
                            new
                            {
                                response = "Failed to upload. Error: " + exp.Message,
                                status = (int)HttpStatusCode.BadRequest
                            }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { response = "Failed to upload. Error: ", status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete Employee
        /// <summary>
        /// Delete Employee Data from DB
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Json for toast message</returns>
        [SiteAuthorize(PermissionKey = "EmployeeDelete")]
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