using System.Web.Mvc;

namespace EPMS.Web.Controllers
{
    public class NotificationController : BaseController
    {
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Detail(long id)
        {
            return View();
        }
    }
}