using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.PMS;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Tasks;
using EPMS.WebBase.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.PMS.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "PMS", IsModule = true)]
    public class TaskController : BaseController
    {
        private readonly ICustomerService CustomerService;
        private readonly IProjectService ProjectService;
        private readonly IProjectTaskService TaskService;
        private readonly IEmployeeService EmployeeService;

        public TaskController(ICustomerService customerService, IProjectService projectService, IProjectTaskService taskService, IEmployeeService employeeService)
        {
            CustomerService = customerService;
            ProjectService = projectService;
            TaskService = taskService;
            EmployeeService = employeeService;
        }

        // GET: PMS/Task
        public ActionResult Index()
        {
            var test = Session["UserPermissionSet"];
            var roles = (string[])test;
            if (roles.Contains("TaskIndex") && roles.Contains("CreateTask"))
            {
                TaskListViewModel viewModel = new TaskListViewModel();
                ViewBag.MessageVM = TempData["message"] as MessageViewModel;
                return View(viewModel);
            }
            return RedirectToAction("MyTasks");
        }

        [HttpPost]
        public ActionResult Index(TaskSearchRequest searchRequest)
        {
            TaskListViewModel viewModel = new TaskListViewModel();
            searchRequest.SearchString = Request["search"];
            if (Session["RoleName"].ToString() == "Customer")
            {
                var customerId = (long)Session["CustomerID"];
                TaskResponse tasks = TaskService.GetProjectTasksForCustomer(searchRequest, customerId);
                IEnumerable<ProjectTask> projectTaskList =
                    tasks.ProjectTasks.Select(x => x.CreateFromServerToClient()).ToList();
                viewModel = new TaskListViewModel
                {
                    aaData = projectTaskList,
                    iTotalRecords = Convert.ToInt32(tasks.TotalRecords),
                    iTotalDisplayRecords = Convert.ToInt32(tasks.TotalDisplayRecords),
                    sEcho = searchRequest.sEcho
                };
            }
            else
            {
                TaskResponse tasks = TaskService.GetAllTasks(searchRequest);
                IEnumerable<ProjectTask> projectTaskList =
                    tasks.ProjectTasks.Select(x => x.CreateFromServerToClient()).ToList();
                viewModel = new TaskListViewModel
                {
                    aaData = projectTaskList,
                    iTotalRecords = Convert.ToInt32(tasks.TotalRecords),
                    iTotalDisplayRecords = Convert.ToInt32(tasks.TotalDisplayRecords),
                    sEcho = searchRequest.sEcho
                };
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MyTasks()
        {
            TaskListViewModel viewModel = new TaskListViewModel();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult MyTasks(TaskSearchRequest searchRequest)
        {
            long employeeId;
            var role = Session["RoleName"].ToString();
            if (role == "Customer")
            {
                employeeId = (long)Session["CustomerID"];
            }
            else
            {
                employeeId = (long)Session["EmployeeID"];
            }
            TaskResponse tasks = TaskService.GetProjectTasksForEmployee(searchRequest,employeeId);
            IEnumerable<ProjectTask> projectTaskList =
                tasks.ProjectTasks.Select(x => x.CreateFromServerToClient()).ToList();
            TaskListViewModel viewModel = new TaskListViewModel
            {
                aaData = projectTaskList,
                iTotalRecords = Convert.ToInt32(tasks.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(tasks.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        [SiteAuthorize(PermissionKey = "CreateTask,TaskDetails")]
        public ActionResult Create(long? id)
        {
            TaskCreateViewModel viewModel = new TaskCreateViewModel();
            var direction = Resources.Shared.Common.TextDirection;
            TaskResponse response = id != null ? TaskService.GetResponseForAddEdit((long) id) : TaskService.GetResponseForAddEdit(0);
            ViewBag.Customers = response.Customers.Select(x => x.CreateFromServerToClient());
            if (id == null)
            {
                viewModel.AllEmployees = response.Employees.Select(x => x.CreateFromServerToClientForTask());
                viewModel.ProjectsForDdls = response.Projects.Select(x => x.CreateFromServerToClientForDdl());
                viewModel.PageTitle = Resources.PMS.Task.PageTitleCreate;
                viewModel.BtnText = Resources.PMS.Task.BtnTextCreate;
                viewModel.Header = Resources.PMS.Task.Create;
                return View(viewModel);
            }
            viewModel.ProjectTask = response.ProjectTask.CreateFromServerToClient();
            viewModel.OldRequisitTasks = viewModel.ProjectTask.RequisitTasks.Select(x => x.TaskId).ToList();
            viewModel.PreRequisitTasks = viewModel.ProjectTask.RequisitTasks.ToList();
            viewModel.OldAssignedEmployees = viewModel.ProjectTask.TaskEmployees.Select(x => x.EmployeeId).ToList();
            viewModel.ProjectsForDdls = response.Projects.Select(x => x.CreateFromServerToClientForDdl());
            viewModel.ProjectAllTasks = response.ProjectTasks.Select(x => x.CreateFromServerToClient());
            viewModel.AllEmployees = response.Employees.Select(x => x.CreateFromServerToClientForTask());
            viewModel.BtnText = Resources.PMS.Task.BtnTextEdit;
            string userRole = (string)Session["RoleName"];
            string taskName = "";
            if (direction == "ltr")
            {
                taskName = viewModel.ProjectTask.TaskNameE;
            }
            else if (direction == "rtl")
            {
                taskName = viewModel.ProjectTask.TaskNameA;
            }
            if (userRole == "Customer")
            {
                viewModel.PageTitle = taskName + Resources.PMS.Task.PageTitleDetail;
                viewModel.Header = Resources.PMS.Task.Detail;
            }
            else
            {
                viewModel.PageTitle = Resources.PMS.Task.PageTitleEdit + taskName;
                viewModel.Header = Resources.PMS.Task.Edit;
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TaskCreateViewModel viewModel)
        {
            if (Request.Form["Save"] != null)
            {
                // Add/Update Task
                if (viewModel.ProjectTask.TaskId > 0)
                {
                    // Update Case
                    viewModel.ProjectTask.RecLastUpdatedBy = User.Identity.GetUserId();
                    viewModel.ProjectTask.RecLastUpdatedDt = DateTime.Now;
                    var projectTaskToUpdate = viewModel.ProjectTask.CreateFromClientToServer();
                    if (TaskService.UpdateProjectTask(projectTaskToUpdate, viewModel.OldRequisitTasks, viewModel.RequisitTasks, viewModel.OldAssignedEmployees, viewModel.AssignedEmployees))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.PMS.Task.UpdateMessage,
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Add case
                    viewModel.ProjectTask.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.ProjectTask.RecCreatedDt = DateTime.Now;
                    var projectTaskToAdd = viewModel.ProjectTask.CreateFromClientToServer();
                    if (TaskService.AddProjectTask(projectTaskToAdd, viewModel.RequisitTasks, viewModel.AssignedEmployees))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.PMS.Task.AddMessage,
                            IsSaved = true
                        };
                        return RedirectToAction("Index");
                    }
                }
            }
            if (Request.Form["Delete"] != null)
            {
                // Delete Task
                try
                {
                    var taskId = viewModel.ProjectTask.TaskId;
                    TaskService.DeleteProjectTask(taskId);
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.PMS.Task.DeleteMessage,
                        IsUpdated = true
                    };
                    ViewBag.MessageVM = TempData["message"] as MessageViewModel;
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.PMS.Task.ErrorDelete,
                        IsError = true
                    };
                    ViewBag.MessageVM = TempData["message"] as MessageViewModel;
                }
            }
            TaskResponse response = TaskService.GetResponseForAddEdit(viewModel.ProjectTask.TaskId);
            // Error
            ViewBag.Customers = response.Customers.Select(x => x.CreateFromServerToClient());
            viewModel.ProjectTask = response.ProjectTask.CreateFromServerToClient();
            viewModel.ProjectsForDdls = response.Projects.Select(x => x.CreateFromServerToClientForDdl());
            viewModel.ProjectAllTasks = response.ProjectTasks.Select(x => x.CreateFromServerToClient());
            viewModel.AllEmployees = response.Employees.Select(x => x.CreateFromServerToClientForTask());
            return View(viewModel);
        }

        [HttpGet]
        public JsonResult GetCustomerProjects(long customerId)
        {
            if (customerId == 0)
            {
                var projects = ProjectService.GetAllProjects().Select(x => x.CreateFromServerToClient());
                return Json(projects, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var projects = ProjectService.FindProjectByCustomerId(customerId).Select(x => x.CreateFromServerToClient());
                return Json(projects, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProjectTasks(long projectId)
        {
            var projects = TaskService.FindProjectTaskByProjectId(projectId, 0).Select(x => x.CreateFromServerToClient());
            return Json(projects.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}