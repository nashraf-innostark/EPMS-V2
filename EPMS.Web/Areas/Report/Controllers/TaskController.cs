using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.PMS;
using EPMS.WebModels.ViewModels.Reports;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class TaskController : BaseController
    {
        private readonly IProjectTaskService taskService;
        private readonly IReportService reportService;

        public TaskController(IProjectTaskService taskService, IReportService reportService)
        {
            this.taskService = taskService;
            this.reportService = reportService;
        }

        // GET: Report/Task
        [SiteAuthorize(PermissionKey = "CreateTaskReport")]
        public ActionResult Create()
        {
            var response = taskService.GetAllProjectsAndTasks();
            TaskReportsCreateViewModel model = new TaskReportsCreateViewModel
            {
                Tasks = response.Tasks.Select(x => x.CreateFoDropDown()).ToList(),
                Projects = response.Projects.Select(x=>x.CreateForDashboardDDL()).ToList()
            };
            return View(model);
        }

        public ActionResult Details(TaskReportsCreateViewModel viewModel)
        {
            TaskReportDetailVeiwModel detailVeiwModel = new TaskReportDetailVeiwModel();
            var request = new TaskReportCreateOrDetailsRequest
            {
                ProjectId = viewModel.ProjectId,
                TaskId = viewModel.TaskId,
                ReportId = viewModel.ReportId,
                RequesterId = Session["UserID"].ToString()
            };
            if (viewModel.ReportId == 0)
                request.IsCreate = true;
            var response = reportService.SaveAndGetTaskReportDetails(request);
            detailVeiwModel.ProjectTasks = response.ProjectTasks.Select(x => x.CreateFromServerToClientLv()).ToList();
            detailVeiwModel.SubTasks = response.SubTasks.Select(x => x.CreateFromServerToClientLv()).ToList();
            return View(detailVeiwModel);
        }
    }
}