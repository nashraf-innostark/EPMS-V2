using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.PMS;
using EPMS.WebModels.ViewModels.Project;
using EPMS.WebModels.ViewModels.Reports;
using EPMS.WebModels.WebsiteModels;
using Rotativa;

namespace EPMS.Web.Areas.Report.Controllers
{
    [Authorize]
    //[SiteAuthorize(PermissionKey = "Reports", IsModule = true)]
    public class WarehouseController : BaseController
    {
        private readonly IProjectService projectService;
        private readonly IReportService reportService;

        public WarehouseController(IProjectService projectService, IReportService reportService)
        {
            this.projectService = projectService;
            this.reportService = reportService;
        }

        //[SiteAuthorize(PermissionKey = "GenerateProjectsReport")]
        public ActionResult All(long? ReportId)
        {
            var request = new ProjectReportCreateOrDetailsRequest();
            if(ReportId!=null)
            {
                request.ReportId = (long)ReportId;
                request.RequesterRole = "Admin";
                request.RequesterId = Session["UserID"].ToString();
                TempData["Projects"] = reportService.SaveAndGetAllProjectsReport(request).ToList().Select(x => x.CreateForReport());
            }
            
            var projects = TempData["Projects"] as IEnumerable<Project>;
            if (projects == null)
                return RedirectToAction("Index", "ProjectsAndTasks");
            return View(TempData["Projects"] as IEnumerable<Project>);
        }
        //[SiteAuthorize(PermissionKey = "GenerateProjectsReport")]
        public ActionResult Create()
        {
            ProjectsReportsCreateViewModel projectsReportsCreateViewModel=new ProjectsReportsCreateViewModel
            {
                Projects = projectService.GetAllProjects().ToList().Select(x => x.CreateForDashboardDDL()).ToList()
            };
            return View(projectsReportsCreateViewModel);
        }
        //[SiteAuthorize(PermissionKey = "DetailsSingleProjectReport")]
        public ActionResult Details(ProjectsReportsCreateViewModel projectsReportsCreateViewModel)
        {
            var request = new ProjectReportCreateOrDetailsRequest
            {
                ProjectId = projectsReportsCreateViewModel.ProjectId,
                ReportId = projectsReportsCreateViewModel.ReportId,
                RequesterRole = Session["RoleName"].ToString(),
                RequesterId = Session["UserID"].ToString()
            };
            //Check if request came from "Report Create Page"
            //var refrel = Request.UrlReferrer;
            //if (refrel != null && refrel.ToString().Contains("Report/Project/Create"))
            //    request.IsCreate = true;
            if (projectsReportsCreateViewModel.ReportId==0)
                    request.IsCreate = true;

            if (projectsReportsCreateViewModel.ProjectId == 0 && projectsReportsCreateViewModel.ReportId == 0)
            {
                TempData["Projects"] = reportService.SaveAndGetAllProjectsReport(request).ToList().Select(x => x.CreateForReport());

                return RedirectToAction("All");
            }
              
            ProjectReportDetailVeiwModel detailVeiwModel = new ProjectReportDetailVeiwModel();
            
            if (projectsReportsCreateViewModel.ProjectId > 0 || projectsReportsCreateViewModel.ReportId > 0)
            {
                var response = reportService.SaveAndGetProjectReportDetails(request);
                detailVeiwModel.Projects = response.Projects.Select(x => x.CreateForReportDetails()).ToList();
                detailVeiwModel.ProjectTasks = response.ProjectTasks.Select(x => x.CreateForReport()).ToList();
                detailVeiwModel.ReportId = response.ReportId;
            }
            return View(detailVeiwModel);
        }
    }
}