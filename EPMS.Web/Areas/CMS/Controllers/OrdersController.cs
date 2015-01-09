using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Orders;
using Microsoft.AspNet.Identity;

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
            viewModel.PageTitle = direction == "ltr" ? "Create New Order" : "";
            viewModel.BtnText = direction == "ltr" ? "Request A Quote" : "";
            return View(viewModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(OrdersCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
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
                    // Add Case
                    viewModel.Orders.OrderDate = DateTime.Now;
                    viewModel.Orders.CustomerId = Convert.ToInt64(User.Identity.GetUserId());
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
            }
            TempData["message"] = new MessageViewModel
            {
                Message = "Please fill all the fields correctly",
                IsSaved = true
            };
            return View(viewModel);
        }

        #endregion
    }
}