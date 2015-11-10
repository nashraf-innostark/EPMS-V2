using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.PMS;
using EPMS.WebModels.ViewModels.Reports;
using Rotativa;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;
        private readonly IReportService reportService;

        public ProjectController(IProjectService projectService, IReportService reportService)
        {
            this.projectService = projectService;
            this.reportService = reportService;
        }

        // GET: Report/Project
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ProjectsReportsCreateViewModel projectsReportsCreateViewModel=new ProjectsReportsCreateViewModel
            {
                Projects = projectService.GetAllProjects().ToList().Select(x => x.CreateForDashboardDDL()).ToList()
            };
            return View(projectsReportsCreateViewModel);
        }

        public ActionResult Details(ProjectsReportsCreateViewModel projectsReportsCreateViewModel)
        {
            ProjectReportDetailVeiwModel detailVeiwModel = new ProjectReportDetailVeiwModel();

            var request = new ProjectReportCreateOrDetailsRequest
            {
                ProjectId = projectsReportsCreateViewModel.ProjectId,
                ReportId = projectsReportsCreateViewModel.ReportId,
                RequesterRole = "Admin",
                RequesterId = Session["UserID"].ToString()
            };

            //Check if request came from "Report Create Page"
            var refrel=Request.UrlReferrer;
            if (refrel != null && refrel.ToString().Contains("Report/Project/Create"))
                request.IsCreate = true;
            if (projectsReportsCreateViewModel.ProjectId > 0 || projectsReportsCreateViewModel.ReportId > 0)
            {
                var response = reportService.SaveAndGetProjectReportDetails(request);
                detailVeiwModel.Projects = response.Projects.Select(x => x.CreateForReportDetails()).ToList();
                detailVeiwModel.ProjectTasks = response.ProjectTasks.Select(x => x.CreateForReport()).ToList();
                detailVeiwModel.ReportId = response.ReportId;
            }
            
            return View(detailVeiwModel);
        }
        public ActionResult GeneratePDF(ProjectsReportsCreateViewModel projectsReportsCreateViewModel)
        {
            return new ActionAsPdf("Create") { FileName = "ProjectDetailedReport.pdf" };
        }
    }
}