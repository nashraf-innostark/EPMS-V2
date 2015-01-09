using System.Web.Mvc;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ViewModels.Orders;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class OrdersController : BaseController
    {
        // GET: HR/Orders
        public ActionResult Index()
        {
            return View(new OrdersListViewModel());
        }
        [HttpPost]
        public ActionResult Index(OrdersSearchRequest searchRequest)
        {
            return View(new OrdersListViewModel());
        }
        public ActionResult Create(long? id)
        {
            OrdersCreateViewModel viewModel = new OrdersCreateViewModel();
            if (id != null)
            {
                
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(OrdersListViewModel viewModel)
        {
            return View(new OrdersCreateViewModel());
        }
    }
}