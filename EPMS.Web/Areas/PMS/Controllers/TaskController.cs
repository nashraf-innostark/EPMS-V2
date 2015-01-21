using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.PMS;
using EPMS.Web.ViewModels.Tasks;

namespace EPMS.Web.Areas.PMS.Controllers
{
    public class TaskController : Controller
    {
        private readonly ICustomerService CustomerService;
        private readonly IProjectService ProjectService;

        public TaskController(ICustomerService customerService, IProjectService projectService)
        {
            CustomerService = customerService;
            ProjectService = projectService;
        }

        // GET: PMS/Task
        public ActionResult Index()
        {
            TaskListViewModel viewModel = new TaskListViewModel();
            return View(viewModel);
        }

        public ActionResult Create(long? id)
        {
            TaskCreateViewModel viewModel = new TaskCreateViewModel();
            var customers = CustomerService.GetAll();
            ViewBag.Customers = customers.Select(x => x.CreateFromServerToClient());
            if (id == null)
            {
                viewModel.PageTitle = Resources.PMS.Task.PageTitleCreate;
                viewModel.BtnText = Resources.PMS.Task.BtnTextCreate;
                viewModel.Header = Resources.PMS.Task.Create;
                return View(viewModel);
            }
            viewModel.PageTitle = Resources.PMS.Task.PageTitleEdit;
            viewModel.BtnText = Resources.PMS.Task.BtnTextEdit;
            viewModel.Header = Resources.PMS.Task.Edit;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TaskCreateViewModel viewModel)
        {
            return View(viewModel);
        }
        [HttpGet]
        public JsonResult GetCustomerProjects(long customerId)
        {
            var projects = ProjectService.FindProjectByCustomerId(customerId).Select(x => x.CreateFromServerToClient());
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
    }
}