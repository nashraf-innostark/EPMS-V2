using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}