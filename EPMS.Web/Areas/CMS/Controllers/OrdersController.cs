using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Orders;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class OrdersController : BaseController
    {
        #region Private
        private readonly IOrdersService OrdersService;
        #endregion

        #region Constructor
        public OrdersController(IOrdersService ordersService)
        {
            OrdersService = ordersService;
        }
        #endregion
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
            var direction = Resources.Shared.Common.TextDirection;
            OrdersCreateViewModel viewModel = new OrdersCreateViewModel();
            if (id != null)
            {
                viewModel.Orders = OrdersService.GetOrderByOrderId((long)id).CreateFromServerToClient();
                viewModel.PageTitle = direction == "ltr" ? "Update Order" : "";
                viewModel.BtnText = direction == "ltr" ? "Update Quote" : "";
                return View(viewModel);
            }
            viewModel.PageTitle = direction == "ltr" ? "Create New Order" : "";
            viewModel.BtnText = direction == "ltr" ? "Request A Quote" : "";
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(OrdersListViewModel viewModel)
        {
            return View(new OrdersCreateViewModel());
        }
    }
}