using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Implementation.Services;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.Reports;
using EPMS.WebModels.ViewModels.Employee;
using EPMS.WebModels.ViewModels.Reports;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Report.Controllers
{
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

        [HttpPost]
        public ActionResult TaskIndex(TaskReportSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
        }
    }
}