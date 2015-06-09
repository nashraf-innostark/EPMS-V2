using System.Web.Mvc;
using EPMS.Web.Controllers;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class PurchaseOrderController : BaseController
    {
        // GET: Inventory/PurchaseOrder
        public ActionResult Index()
        {
            return View();
        }
    }
}