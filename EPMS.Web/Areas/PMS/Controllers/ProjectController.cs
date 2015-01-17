using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.PMS;
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

        // GET: PMS/Project/OnGoing
        public ActionResult OnGoing()
        {
            ProjectListViewModel projectsList=new ProjectListViewModel();
            projectsList.Projects = projectService.LoadAllOnGoingProjects().Select(x=>x.CreateFromServerToClient());
            return View(projectsList);
        }
        // GET: PMS/Project/Finished
        public ActionResult Finished()
        {
            ProjectListViewModel projectsList = new ProjectListViewModel();
            projectsList.Projects = projectService.LoadAllFinishedProjects().Select(x => x.CreateFromServerToClient());
            return View(projectsList);
        }
    }
}