using System.Web.Mvc;
using EPMS.Web.ViewModels.Tasks;

namespace EPMS.Web.Areas.PMS.Controllers
{
    public class TaskController : Controller
    {
        // GET: PMS/Task
        public ActionResult Index()
        {
            TaskListViewModel viewModel = new TaskListViewModel();
            return View(viewModel);
        }

        public ActionResult Create(long? id)
        {
            TaskCreateViewModel viewModel = new TaskCreateViewModel();
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
    }
}