using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ViewModels.Orders;
using EPMS.Web.Controllers;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Order = EPMS.WebModels.WebsiteModels.Order;

namespace EPMS.Web.Areas.CMS.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "CS", IsModule = true)]
    public class OrdersController : BaseController
    {
        #region Private
        private readonly IOrdersService ordersService;
        #endregion

        #region Constructor
        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        #endregion

        #region Public

        #region ListView

        // GET: HR/Orders
        [SiteAuthorize(PermissionKey = "OrdersIndex")]
        public ActionResult Index()
        {
            OrdersListViewModel viewModel = new OrdersListViewModel();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        /// <summary>
        /// Get List of Orders for ListView
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(OrdersSearchRequest searchRequest)
        {
            searchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
            searchRequest.SearchString = Request["search"];
            OrdersResponse ordersList = null;
            IEnumerable<Order> orders = null;
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            if (!userPermissionsSet.Contains("OrderCreate"))
            {
                searchRequest.CustomerId = 0;
                ordersList = ordersService.GetAllOrders(searchRequest);
                orders = ordersList.Orders.Select(o => o.CreateFromServerToClientLv());
            }
            if (userPermissionsSet.Contains("OrderCreate"))
            {
                searchRequest.CustomerId = Convert.ToInt64(Session["CustomerID"]);
                ordersList = ordersService.GetAllOrders(searchRequest);
                orders = ordersList.Orders.Select(o => o.CreateFromServerToClientLv());
            }
            if (ordersList == null) return View(new OrdersListViewModel());
            OrdersListViewModel viewModel = new OrdersListViewModel
            {
                aaData = orders,
                iTotalRecords = Convert.ToInt32(ordersList.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(ordersList.TotalDisplayRecords),
                SearchRequest = searchRequest
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create/Update

        /// <summary>
        /// Get Order by Id or return Create new Order View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SiteAuthorize(PermissionKey = "OrderCreate,OrderDetails")]
        public ActionResult Create(long? id)
        {
            OrdersCreateViewModel viewModel = new OrdersCreateViewModel();
            if (Request.UrlReferrer != null)
            {
                ViewBag.backUrl = Request.UrlReferrer;
            }
            else
            {
                ViewBag.backUrl = "";
            }
            int orderId = 0;
            if (id != null)
            {
                orderId = (int) id;
            }
            OrdersResponse response = ordersService.GetOrderResponse(orderId);
            if (id != null )
            {
                viewModel.Orders = response.Order.CreateFromServerToClient();
                if (Session["RoleName"].ToString() == "Customer")
                {
                    viewModel.PageTitle = WebModels.Resources.CMS.Order.PTCreateUpdate;
                }
                else if (Session["RoleName"].ToString() == "Admin")
                {
                    viewModel.PageTitle = WebModels.Resources.CMS.Order.OrderDetail;
                }
                viewModel.BtnText = WebModels.Resources.CMS.Order.BtnUpdate;
                return View(viewModel);
            }
            viewModel.Orders.OrderNo = Utility.GetOrderNumber(response.Orders.ToList());
            viewModel.PageTitle = WebModels.Resources.CMS.Order.PTCreateSave;
            viewModel.BtnText = WebModels.Resources.CMS.Order.BtnSave;
            return View(viewModel);
        }

        /// <summary>
        /// Add or Update Order
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [SiteAuthorize(PermissionKey = "OrderCreate")]
        public ActionResult Create(OrdersCreateViewModel viewModel)
        {
            if (viewModel.Orders.OrderId > 0)
            {
                // Update Case
                viewModel.Orders.RecLastUpdatedBy = User.Identity.GetUserId();
                viewModel.Orders.RecLastUpdatedDate = DateTime.Now;
                var orderToUpdate = viewModel.Orders.CreateFromClientToServer();
                if (ordersService.UpdateOrder(orderToUpdate))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = WebModels.Resources.CMS.Order.Updated,
                        IsSaved = true
                    };
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Add Case
                viewModel.Orders.CustomerId = Convert.ToInt64(Session["CustomerID"]);
                viewModel.Orders.OrderStatus = 2;
                viewModel.Orders.RecCreatedBy = User.Identity.GetUserId();
                viewModel.Orders.RecCreatedDate = DateTime.Now;
                viewModel.Orders.RecLastUpdatedBy = User.Identity.GetUserId();
                viewModel.Orders.RecLastUpdatedDate = DateTime.Now;
                var orderToSave = viewModel.Orders.CreateFromClientToServer();
                if (ordersService.AddOrder(orderToSave))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = WebModels.Resources.CMS.Order.Added,
                        IsSaved = true
                    };
                    return RedirectToAction("Index");
                }
            }
            if (Session["RoleName"].ToString() == "Admin")
            {
                viewModel.PageTitle = WebModels.Resources.CMS.Order.PTCreateUpdate;
            }
            if (Session["RoleName"].ToString() == "Customer")
            {
                viewModel.PageTitle = WebModels.Resources.CMS.Order.PTCreateSave;
            }
            viewModel.BtnText = WebModels.Resources.CMS.Order.BtnSave;
            TempData["message"] = new MessageViewModel
            {
                Message = WebModels.Resources.CMS.Order.Error,
                IsError = true
            };
            return View(viewModel);
        }

        #endregion

        #endregion
    }
}