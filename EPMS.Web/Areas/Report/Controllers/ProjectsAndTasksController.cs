using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers.Reports;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Reports;

namespace EPMS.Web.Areas.Report.Controllers
{
    [SiteAuthorize(PermissionKey = "ProjectsTasksReport", IsModule = true)]
    public class ProjectsAndTasksController : BaseController
    {
        private readonly IReportService reportService;
        public ProjectsAndTasksController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [SiteAuthorize(PermissionKey = "ProjectsTasksReport")]
        public ActionResult Index()
        {
            var projectsReports = new ListViewModel();

            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;
            return View(projectsReports);
        }
        [HttpPost]
        public ActionResult Index(ProjectReportSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var projectsAndTasksResponse = reportService.GetProjectsReports(searchRequest);
            var projectsList =
                projectsAndTasksResponse.Reports.Select(x => x.CreateProjectReportFromServerToClient()).ToList();
            ListViewModel projectsListViewModel = new ListViewModel
            {
                aaData = projectsList,
                iTotalRecords = Convert.ToInt32(projectsAndTasksResponse.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(projectsAndTasksResponse.FilteredCount),
                sEcho = searchRequest.sEcho
            };
            return Json(projectsListViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TaskIndex(TaskReportSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var tasksResponse = reportService.GetTasksReports(searchRequest);
            var tasksList =
                tasksResponse.Tasks.Select(x => x.CreateTaskReportFromServerToClient()).ToList();
            TasksReportListViewModel tasksListViewModel = new TasksReportListViewModel
            {
                aaData = tasksList,
                iTotalRecords = Convert.ToInt32(tasksResponse.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(tasksResponse.FilteredCount),
                sEcho = searchRequest.sEcho
            };
            return Json(tasksListViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}