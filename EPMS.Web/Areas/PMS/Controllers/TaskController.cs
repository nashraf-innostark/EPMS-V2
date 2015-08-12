using System;
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
using EPMS.Web.Resources.PMS;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Tasks;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.PMS.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "PMS", IsModule = true)]
    public class TaskController : BaseController
    {
        #region Private

        private readonly IProjectService ProjectService;
        private readonly IProjectTaskService TaskService;

        #endregion


        #region Constructor

        public TaskController(IProjectService projectService, IProjectTaskService taskService)
        {
            ProjectService = projectService;
            TaskService = taskService;
        }

        #endregion


        #region Task ListView for Admin

        // GET: PMS/Task
        [SiteAuthorize(PermissionKey = "TaskIndex")]
        public ActionResult Index()
        {
            //var test = Session["UserPermissionSet"];
            //var roles = (string[])test;
            //if (roles.Contains("TaskIndex") && roles.Contains("CreateTask"))
            //{
            TaskListViewModel viewModel = new TaskListViewModel();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
            //}
            //return RedirectToAction("MyTasks");
        }

        /// <summary>
        /// Get All tasks for Admin
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(TaskSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            if (userPermissionsSet.Contains("ListviewAllTasks"))
            {
                searchRequest.AllowedAll = true;
            }
            else
            {
                searchRequest.AllowedAll = false;
                searchRequest.UserId = Session["UserID"].ToString();
            }
            TaskResponse tasks = TaskService.GetAllTasks(searchRequest);
            IEnumerable<ProjectTask> projectTaskList =
                tasks.ProjectTasks.Where(x => x.ParentTask == null).Select(x => x.CreateFromServerToClientLv()).ToList();
            TaskListViewModel viewModel = new TaskListViewModel
            {
                aaData = projectTaskList,
                iTotalRecords = Convert.ToInt32(tasks.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(tasks.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Task ListView Specific to User

        /// <summary>
        /// Get Tasks specific to User
        /// </summary>
        /// <returns></returns>
        [SiteAuthorize(PermissionKey = "MyTasks")]
        public ActionResult MyTasks()
        {
            TaskListViewModel viewModel = new TaskListViewModel();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult MyTasks(TaskSearchRequest searchRequest)
        {
            long employeeId = (long)Session["EmployeeID"];
            TaskResponse tasks = TaskService.GetProjectTasksForEmployee(searchRequest, employeeId);
            IEnumerable<ProjectTask> projectTaskList = tasks.ProjectTasks.Select(z => z.CreateFromServerToClientLv()).ToList();
            TaskListViewModel viewModel = new TaskListViewModel
            {
                aaData = projectTaskList,
                iTotalRecords = Convert.ToInt32(projectTaskList.Count()),
                iTotalDisplayRecords = Convert.ToInt32(projectTaskList.Count()),
                sEcho = searchRequest.sEcho
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Add/Update Task

        /// <summary>
        /// Get Task by Id or return view to Add new Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SiteAuthorize(PermissionKey = "CreateTask,TaskDetails")]
        public ActionResult Create(long? id)
        {
            TaskCreateViewModel viewModel = new TaskCreateViewModel();
            var direction = Resources.Shared.Common.TextDirection;
            TaskResponse response = id != null
                ? TaskService.GetResponseForAddEdit((long)id)
                : TaskService.GetResponseForAddEdit(0);
            ViewBag.Customers = response.Customers.Select(x => x.CreateFromServerToClient());
            viewModel.AllParentTasks = response.AllParentTasks.Select(x => x.CreateFromServerToClientParentTasks());
            if (id == null)
            {
                viewModel.AllEmployees = response.Employees.Select(x => x.CreateFromServerToClientForTask());
                viewModel.ProjectsForDdls = response.Projects.Select(x => x.CreateFromServerToClientForDdl());
                viewModel.PageTitle = Task.PageTitleCreate;
                viewModel.BtnText = Task.BtnTextCreate;
                viewModel.Header = Task.Create;
                return View(viewModel);
            }
            viewModel.ProjectTask = response.ProjectTask.CreateFromServerToClientCreate();
            viewModel.OldRequisitTasks = viewModel.ProjectTask.RequisitTasks.Select(x => x.TaskId).ToList();
            viewModel.PreRequisitTasks = viewModel.ProjectTask.RequisitTasks.ToList();
            viewModel.OldAssignedEmployees = viewModel.ProjectTask.TaskEmployees.Select(x => x.EmployeeId).ToList();
            viewModel.ProjectsForDdls = response.Projects.Select(x => x.CreateFromServerToClientForDdl());
            //viewModel.ProjectAllTasks = response.ProjectTasks.Select(x => x.CreateFromServerToClient());
            viewModel.ProjectAllTasks = response.ProjectTasks.Where(x => x.ParentTask == null).Select(x => x.CreateFromServerToClientCreate());
            viewModel.AllEmployees = response.Employees.Select(x => x.CreateFromServerToClientForTask());
            viewModel.BtnText = Task.BtnTextEdit;
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
                viewModel.PageTitle = taskName + Task.PageTitleDetail;
                viewModel.Header = Task.Detail;
            }
            else
            {
                viewModel.PageTitle = Task.PageTitleEdit + taskName;
                viewModel.Header = Task.Edit;
            }
            return View(viewModel);
        }

        /// <summary>
        /// Add or Update Task
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [SiteAuthorize(PermissionKey = "CreateTask")]
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
                    if (TaskService.UpdateProjectTask(projectTaskToUpdate, viewModel.OldRequisitTasks,
                        viewModel.RequisitTasks, viewModel.OldAssignedEmployees, viewModel.AssignedEmployees))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Task.UpdateMessage,
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    if (viewModel.ProjectTask.IsParent)
                    {
                        viewModel.AssignedEmployees = new List<long>();
                    }
                    // Add case
                    viewModel.ProjectTask.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.ProjectTask.RecCreatedDt = DateTime.Now;
                    if (viewModel.ProjectTask.IsParent)
                    {
                        viewModel.ProjectTask.TaskProgress = "0";
                    }
                    var projectTaskToAdd = viewModel.ProjectTask.CreateFromClientToServer();
                    if (TaskService.AddProjectTask(projectTaskToAdd, viewModel.RequisitTasks,
                        viewModel.AssignedEmployees))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Task.AddMessage,
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
                        Message = Task.DeleteMessage,
                        IsUpdated = true
                    };
                    ViewBag.MessageVM = TempData["message"] as MessageViewModel;
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Task.ErrorDelete,
                        IsError = true
                    };
                    ViewBag.MessageVM = TempData["message"] as MessageViewModel;
                }
            }
            TaskResponse response = TaskService.GetResponseForAddEdit(viewModel.ProjectTask.TaskId);
            // Error
            viewModel.AllParentTasks = response.AllParentTasks.Select(x => x.CreateFromServerToClientParentTasks());
            ViewBag.Customers = response.Customers.Select(x => x.CreateFromServerToClient());
            viewModel.ProjectTask = response.ProjectTask != null ? response.ProjectTask.CreateFromServerToClientCreate() : new ProjectTask();
            viewModel.ProjectsForDdls = response.Projects.Select(x => x.CreateFromServerToClientForDdl());
            viewModel.ProjectAllTasks = response.ProjectTasks.Select(x => x.CreateFromServerToClientCreate());
            viewModel.AllEmployees = response.Employees.Select(x => x.CreateFromServerToClientForTask());
            viewModel.BtnText = Task.BtnTextEdit;
            string userRole = (string)Session["RoleName"];
            string taskName = "";
            if (userRole == "Customer")
            {
                viewModel.PageTitle = taskName + Task.PageTitleDetail;
                viewModel.Header = Task.Detail;
            }
            else
            {
                viewModel.PageTitle = Task.PageTitleEdit + taskName;
                viewModel.Header = Task.Edit;
            }
            TempData["message"] = new MessageViewModel
            {
                Message = Task.ErrorSaving,
                IsError = true
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        #endregion


        #region Get Projects by CustomerId

        /// <summary>
        /// Get Projects by CustomerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
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
                var projects =
                    ProjectService.FindProjectByCustomerId(customerId).Select(x => x.CreateFromServerToClient());
                return Json(projects, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region Get Projects Tasks by ProjectId

        /// <summary>
        /// Get Projects Tasks by ProjectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetProjectTasks(long projectId)
        {
            ParentProjectTaskAndProject taskAndParent = new ParentProjectTaskAndProject
            {
                ProjectTasks = TaskService.FindProjectTaskByProjectId(projectId, 0).Select(x => x.CreateFromServerToClientCreate()).ToList(),
                ParentTasks = TaskService.FindParentTasksByProjectId(projectId).Select(x => x.CreateFromServerToClientCreate()).ToList()
            };
            //var projects = TaskService.FindProjectTaskByProjectId(projectId, 0)
            //    .Select(x => x.CreateFromServerToClient());
            //var parentTasks = TaskService.FindParentTasksByProjectId(projectId)
            //    .Select(x => x.CreateFromServerToClient());
            return Json(taskAndParent, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}