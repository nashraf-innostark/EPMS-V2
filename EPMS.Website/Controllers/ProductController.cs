using System.Web.Mvc;

namespace EPMS.Website.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(long id)
        {
            return View();
        }
    }
}