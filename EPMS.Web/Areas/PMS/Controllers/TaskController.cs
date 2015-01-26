using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.PMS;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Tasks;
using EPMS.WebBase.Mvc;
using iTextSharp.text;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.PMS.Controllers
{
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
        [SiteAuthorize(PermissionKey = "TaskIndex")]
        public ActionResult Index()
        {
            TaskListViewModel viewModel = new TaskListViewModel();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(TaskSearchRequest searchRequest)
        {
            var tasks = TaskService.GetAllTasks(searchRequest);
            IEnumerable<ProjectTask> projectTaskList =
                tasks.ProjectTasks.Select(x => x.CreateFromServerToClient()).ToList();
            TaskListViewModel viewModel = new TaskListViewModel
            {
                aaData = projectTaskList,
                iTotalRecords = Convert.ToInt32(tasks.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(tasks.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet); ;
        }
        [SiteAuthorize(PermissionKey = "CreateTask")]
        public ActionResult Create(long? id)
        {
            TaskCreateViewModel viewModel = new TaskCreateViewModel();
            var customers = CustomerService.GetAll();
            ViewBag.Customers = customers.Select(x => x.CreateFromServerToClient());
            if (id == null)
            {
                viewModel.AllEmployees = EmployeeService.GetAll().Select(x => x.CreateFromServerToClient());
                viewModel.PageTitle = Resources.PMS.Task.PageTitleCreate;
                viewModel.BtnText = Resources.PMS.Task.BtnTextCreate;
                viewModel.Header = Resources.PMS.Task.Create;
                return View(viewModel);
            }
            viewModel.ProjectTask = TaskService.FindProjectTaskById((long) id).CreateFromServerToClient();
            viewModel.OldRequisitTasks = viewModel.ProjectTask.RequisitTasks.Select(x => x.TaskId).ToList();
            viewModel.OldAssignedEmployees = viewModel.ProjectTask.TaskEmployees.Select(x => x.EmployeeId).ToList();
            viewModel.Projects = ProjectService.FindProjectByCustomerId(viewModel.ProjectTask.CustomerId).Select(x => x.CreateFromServerToClient());
            viewModel.ProjectAllTasks = TaskService.FindProjectTaskByProjectId(viewModel.ProjectTask.ProjectId).Select(x => x.CreateFromServerToClient());
            viewModel.AllEmployees = EmployeeService.GetAll().Select(x => x.CreateFromServerToClient());
            viewModel.BtnText = Resources.PMS.Task.BtnTextEdit;
            string userRole = (string)Session["RoleName"];
            if (userRole == "Customer")
            {
                viewModel.PageTitle = Resources.PMS.Task.PageTitleDetail;
                viewModel.Header = Resources.PMS.Task.Detail;
            }
            else
            {
                viewModel.PageTitle = Resources.PMS.Task.PageTitleEdit;
                viewModel.Header = Resources.PMS.Task.Edit;
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TaskCreateViewModel viewModel)
        {
            if (viewModel.ProjectTask.TaskId > 0)
            {
                viewModel.ProjectTask.RecLastUpdatedBy = User.Identity.GetUserId();
                viewModel.ProjectTask.RecLastUpdatedDt = DateTime.Now;
                var projectTaskToUpdate = viewModel.ProjectTask.CreateFromClientToServer();
                if (TaskService.UpdateProjectTask(projectTaskToUpdate, viewModel.OldRequisitTasks, viewModel.RequisitTasks, viewModel.OldAssignedEmployees, viewModel.AssignedEmployees))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.PMS.Task.AddMessage,
                        IsSaved = true
                    };
                    return RedirectToAction("Index");
                }
            }
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
            var customers = CustomerService.GetAll();
            ViewBag.Customers = customers.Select(x => x.CreateFromServerToClient());
            viewModel.ProjectTask = TaskService.FindProjectTaskById(viewModel.ProjectTask.TaskId).CreateFromServerToClient();
            viewModel.Projects = ProjectService.FindProjectByCustomerId(viewModel.ProjectTask.CustomerId).Select(x => x.CreateFromServerToClient());
            viewModel.ProjectAllTasks = TaskService.FindProjectTaskByProjectId(viewModel.ProjectTask.ProjectId).Select(x => x.CreateFromServerToClient());
            viewModel.AllEmployees = EmployeeService.GetAll().Select(x => x.CreateFromServerToClient());
            return View(viewModel);
        }

        [HttpGet]
        public JsonResult GetCustomerProjects(long customerId)
        {
            var projects = ProjectService.FindProjectByCustomerId(customerId).Select(x => x.CreateFromServerToClient());
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProjectTasks(long projectId)
        {
            var projects = TaskService.FindProjectTaskByProjectId(projectId).Select(x => x.CreateFromServerToClient());
            return Json(projects.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}