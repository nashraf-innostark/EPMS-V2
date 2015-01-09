using System.Web.Mvc;
using EPMS.Models.RequestModels;
using EPMS.Web.ViewModels.Orders;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class OrdersController : Controller
    {
        // GET: HR/Orders
        public ActionResult Index()
        {
            return View(new OrdersViewModel());
        }
        [HttpPost]
        public ActionResult Index(OrdersSearchRequest searchRequest)
        {
            return View(new OrdersViewModel());
        }
        public ActionResult Create(long? id)
        {
            return View(new OrdersViewModel());
        }
        [HttpPost]
        public ActionResult Create(OrdersViewModel viewModel)
        {
            return View(new OrdersViewModel());
        }
    }
}