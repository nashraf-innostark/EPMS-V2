using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Project;

namespace EPMS.Web.Areas.PMS.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        // GET: PMS/Project
        public ActionResult Index()
        {
            ProjectListViewModel projectsList=new ProjectListViewModel();
            projectsList.Projects = projectService.LoadAllProjects().Select(x=>x.c);
            return View(projectsList);
        }
    }
}