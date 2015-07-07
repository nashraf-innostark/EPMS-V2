using System.Web.Mvc;
using EPMS.Web.Controllers;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class PhysicalCountController : BaseController
    {
        // GET: Inventory/PhysicalCount
        public ActionResult Index()
        {
            return View();
        }
    }
}