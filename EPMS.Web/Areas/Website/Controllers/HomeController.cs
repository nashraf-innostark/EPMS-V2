using System.Web.Mvc;
using EPMS.Web.Controllers;
using EPMS.Web.ViewModels.Common;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Website/Home
        public ActionResult Index()
        {

            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View();
        }
    }
}