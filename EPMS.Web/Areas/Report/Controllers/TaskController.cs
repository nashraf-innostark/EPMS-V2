using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.PMS;
using EPMS.WebModels.ViewModels.Reports;
//using RazorPDF;
using Rotativa;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class TaskController : BaseController
    {
        private readonly IProjectTaskService taskService;
        private readonly IReportService reportService;
        private TaskReportDetailsResponse GetReportDetails(TaskReportsCreateViewModel viewModel)
        {
            var request = new TaskReportCreateOrDetailsRequest
            {
                ProjectId = viewModel.ProjectId,
                TaskId = viewModel.TaskId,
                ReportId = viewModel.ReportId,
                RequesterId = Session["UserID"].ToString()
            };
            if (viewModel.ReportId == 0)
                request.IsCreate = true;
            TaskReportDetailsResponse response = reportService.SaveAndGetTaskReportDetails(request);
            return response;
        }

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
            TaskReportDetailsResponse response = GetReportDetails(viewModel);
            TaskReportDetailVeiwModel detailViewModel = new TaskReportDetailVeiwModel
            {
                ReportId = viewModel.ReportId,
                ProjectTasks = response.ProjectTasks.Select(x => x.CreateFromServerToClientLv()).ToList(),
                SubTasks = response.SubTasks.Select(x => x.CreateFromServerToClientLv()).ToList()
            };
            return View(detailViewModel);
        }

        public ActionResult GeneratePdf(TaskReportsCreateViewModel viewModel)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            return new ActionAsPdf("ReportAsPdf", new { ReportId = viewModel.ReportId}) { FileName = "Test.pdf", Cookies = cookieCollection };
        }

        public ActionResult ReportAsPdf(TaskReportsCreateViewModel viewModel)
        {
            TaskReportDetailsResponse response = GetReportDetails(viewModel);
            TaskReportDetailVeiwModel detailViewModel = new TaskReportDetailVeiwModel
            {
                ReportId = viewModel.ReportId,
                ProjectTasks = response.ProjectTasks.Select(x => x.CreateFromServerToClientLv()).ToList(),
                SubTasks = response.SubTasks.Select(x => x.CreateFromServerToClientLv()).ToList()
            };
            return View(detailViewModel);
            //return new RazorPDF.PdfResult(detailViewModel, "ReportAsPdf");
        }

        
    }
}