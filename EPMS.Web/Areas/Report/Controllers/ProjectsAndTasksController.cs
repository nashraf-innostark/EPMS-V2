using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers.Reports;
using EPMS.WebModels.ViewModels.Reports;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class ProjectsAndTasksController : BaseController
    {
        private readonly IReportService reportService;

        public ProjectsAndTasksController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public ActionResult Index()
        {
            var projectsReports = new ProjectsReportsListViewModel();

            return View(projectsReports);
        }
        [HttpPost]
        public ActionResult Index(ProjectReportSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var projectsAndTasksResponse = reportService.GetProjectsReports(searchRequest);
            var projectsList =
                projectsAndTasksResponse.Projects.Select(x => x.CreateProjectReportFromServerToClient()).ToList();
            ProjectsReportsListViewModel projectsListViewModel = new ProjectsReportsListViewModel
            {
                aaData = projectsList,
                iTotalRecords = Convert.ToInt32(projectsAndTasksResponse.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(projectsAndTasksResponse.FilteredCount),
                sEcho = searchRequest.sEcho
            };
            return Json(projectsListViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}