using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Orders;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Order = EPMS.Web.Models.Order;

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

        #region Public
        // GET: HR/Orders
        public ActionResult Index()
        {
            OrdersListViewModel viewModel = new OrdersListViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(OrdersSearchRequest searchRequest)
        {
            searchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
            searchRequest.SearchString = Request["search"];
            var ordersList = OrdersService.GetAllOrders(searchRequest);
            IEnumerable<Order> orders = ordersList.Orders.Select(o => o.CreateFromServerToClient()).ToList();
            OrdersListViewModel viewModel = new OrdersListViewModel
            {
                aaData = orders,
                iTotalRecords = Convert.ToInt32(ordersList.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(ordersList.TotalDisplayRecords),
                SearchRequest = searchRequest
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
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
            viewModel.Orders.OrderNo = GetOrderNumber();
            viewModel.PageTitle = direction == "ltr" ? "Create New Order" : "";
            viewModel.BtnText = direction == "ltr" ? "Request A Quote" : "";
            return View(viewModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(OrdersCreateViewModel viewModel)
        {
            var direction = Resources.Shared.Common.TextDirection;
            if (viewModel.Orders.OrderId > 0)
            {
                // Update Case
                viewModel.Orders.RecLastUpdatedBy = User.Identity.GetUserId();
                viewModel.Orders.RecLastUpdatedDt = DateTime.Now;
                var orderToUpdate = viewModel.Orders.CreateFromClientToServer();
                if (OrdersService.UpdateOrder(orderToUpdate))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Order has been Updated",
                        IsSaved = true
                    };
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Get Customer Id
                AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
                var customerId = result.CustomerId;
                // Add Case
                viewModel.Orders.OrderDate = DateTime.Now;
                viewModel.Orders.CustomerId = customerId ?? 0;
                viewModel.Orders.RecCreatedBy = User.Identity.GetUserId();
                viewModel.Orders.RecCreatedDt = DateTime.Now;
                var orderToSave = viewModel.Orders.CreateFromClientToServer();
                if (OrdersService.AddOrder(orderToSave))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Order has been Added",
                        IsSaved = true
                    };
                    return RedirectToAction("Index");
                }
            }
            viewModel.PageTitle = direction == "ltr" ? "Create New Order" : "";
            viewModel.BtnText = direction == "ltr" ? "Request A Quote" : "";
            TempData["message"] = new MessageViewModel
            {
                Message = "Please fill all the fields correctly",
                IsError = true
            };
            return View(viewModel);
        }

        string GetOrderNumber()
        {
            var result = OrdersService.GetAll();
            var order = result.LastOrDefault();
            if (order != null)
            {
                string oId = order.OrderNo;
                int id = Convert.ToInt32(oId) + 1;
                int len = id.ToString(CultureInfo.InvariantCulture).Length;
                string zeros = "";
                switch (len)
                {
                    case 1:
                        zeros = "000";
                        break;
                    case 2:
                        zeros = "00";
                        break;
                    case 3:
                        zeros = "0";
                        break;
                    case 4:
                        zeros = "";
                        break;
                }
                string orderId = zeros + id.ToString(CultureInfo.InvariantCulture);
                return orderId;
            }
            return "0001";
        }
        #endregion
    }
}