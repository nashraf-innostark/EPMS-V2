using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.PMS;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Tasks;
using iTextSharp.text;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.PMS.Controllers
{
    public class TaskController : Controller
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
            TaskListViewModel viewModel = new TaskListViewModel();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

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
            viewModel.Projects = ProjectService.FindProjectByCustomerId(viewModel.ProjectTask.CustomerId).Select(x => x.CreateFromServerToClient());
            viewModel.ProjectAllTasks = TaskService.FindProjectTaskByProjectId(viewModel.ProjectTask.ProjectId).Select(x => x.CreateFromServerToClient());
            viewModel.AllEmployees = EmployeeService.GetAll().Select(x => x.CreateFromServerToClient());
            viewModel.PageTitle = Resources.PMS.Task.PageTitleEdit;
            viewModel.BtnText = Resources.PMS.Task.BtnTextEdit;
            viewModel.Header = Resources.PMS.Task.Edit;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TaskCreateViewModel viewModel)
        {
            if (viewModel.ProjectTask.TaskId > 0)
            {
                List<long> newPreReqTasks = new List<long>();
                // Check if pre-req task updated
                if (IfPreReqTasksUpdated(viewModel.OldRequisitTasks,viewModel.RequisitTasks))
                {
                    // new added
                    if (viewModel.OldRequisitTasks.Count < viewModel.RequisitTasks.Count)
                    {
                        foreach (var requisitTask in viewModel.RequisitTasks)
                        {
                            if (!viewModel.OldRequisitTasks.Contains(requisitTask))
                            {
                                newPreReqTasks.Add(requisitTask);
                            }
                        }
                    }
                    // removed
                    if (viewModel.OldRequisitTasks.Count > viewModel.RequisitTasks.Count)
                    {

                    }
                    // no change
                    if (viewModel.OldRequisitTasks.Count == viewModel.RequisitTasks.Count)
                    {

                    }
                }
                var projectTaskToUpdate = viewModel.ProjectTask.CreateFromClientToServer();
                if (TaskService.UpdateProjectTask(projectTaskToUpdate, newPreReqTasks))
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
            if (TaskService.AddProjectTask(projectTaskToAdd, viewModel.RequisitTasks))
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

        bool IfPreReqTasksUpdated(List<long> oldTasks, List<long> newTasks )
        {
            if (oldTasks.Count != newTasks.Count)
            {
                for (int i=0; i < oldTasks.Count; i++)
                {
                    if (oldTasks[i] != newTasks[i])
                    {
                        return true;
                    }
                }
                return true;
            }
            return false;
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