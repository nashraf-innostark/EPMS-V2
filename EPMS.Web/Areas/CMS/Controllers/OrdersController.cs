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
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Orders;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Order = EPMS.Web.Models.Order;

namespace EPMS.Web.Areas.CMS.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "CS", IsModule = true)]
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
                ordersList = OrdersService.GetAllOrders(searchRequest);
                orders = ordersList.Orders.Select(o => o.CreateFromServerToClientLv());
            }
            if (userPermissionsSet.Contains("OrderCreate"))
            {
                searchRequest.CustomerId = Convert.ToInt64(Session["CustomerID"]);
                ordersList = OrdersService.GetAllOrders(searchRequest);
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
            var direction = Resources.Shared.Common.TextDirection;
            OrdersCreateViewModel viewModel = new OrdersCreateViewModel();
            if (Request.UrlReferrer != null)
            {
                ViewBag.backUrl = Request.UrlReferrer;
            }
            else
            {
                ViewBag.backUrl = "";
            }
            if (id != null)
            {
                viewModel.Orders = OrdersService.GetOrderByOrderId((long)id).CreateFromServerToClient();
                if (Session["RoleName"].ToString() == "Customer")
                {
                    viewModel.PageTitle = Resources.CMS.Order.PTCreateUpdate;
                }
                else if (Session["RoleName"].ToString() == "Admin")
                {
                    viewModel.PageTitle = Resources.CMS.Order.OrderDetail;
                }
                viewModel.BtnText = Resources.CMS.Order.BtnUpdate;
                return View(viewModel);
            }
            viewModel.Orders.OrderNo = GetOrderNumber();
            viewModel.PageTitle = Resources.CMS.Order.PTCreateSave;
            viewModel.BtnText = Resources.CMS.Order.BtnSave;
            return View(viewModel);
        }

        /// <summary>
        /// Add or Update Order
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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
                        Message = Resources.CMS.Order.Updated,
                        IsSaved = true
                    };
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Add Case

                // Get Customer Id from AspNetUser
                viewModel.Orders.OrderDate = DateTime.Now;
                viewModel.Orders.CustomerId = Convert.ToInt64(Session["CustomerID"]);
                viewModel.Orders.OrderStatus = 2;
                viewModel.Orders.RecCreatedBy = User.Identity.GetUserId();
                viewModel.Orders.RecCreatedDt = DateTime.Now;
                var orderToSave = viewModel.Orders.CreateFromClientToServer();
                if (OrdersService.AddOrder(orderToSave))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.CMS.Order.Added,
                        IsSaved = true
                    };
                    return RedirectToAction("Index");
                }
            }
            if (Session["RoleName"].ToString() == "Admin")
            {
                viewModel.PageTitle = Resources.CMS.Order.PTCreateUpdate;
            }
            if (Session["RoleName"].ToString() == "Customer")
            {
                viewModel.PageTitle = Resources.CMS.Order.PTCreateSave;
            }
            viewModel.BtnText = Resources.CMS.Order.BtnSave;
            TempData["message"] = new MessageViewModel
            {
                Message = Resources.CMS.Order.Error,
                IsError = true
            };
            return View(viewModel);
        }

        #endregion


        #region Get Order Number
        /// <summary>
        /// Get Autogenerated Order number
        /// </summary>
        /// <returns></returns>
        string GetOrderNumber()
        {
            string year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            string month = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture);
            string day = DateTime.Now.Day.ToString(CultureInfo.InvariantCulture);
            var result = OrdersService.GetAll();
            var order = result.LastOrDefault();
            if (order != null)
            {
                string oId = order.OrderNo.Substring(order.OrderNo.Length - 5, 5);
                int id = Convert.ToInt32(oId) + 1;
                int len = id.ToString(CultureInfo.InvariantCulture).Length;
                string zeros = "";
                switch (len)
                {
                    case 1:
                        zeros = "0000";
                        break;
                    case 2:
                        zeros = "000";
                        break;
                    case 3:
                        zeros = "00";
                        break;
                    case 4:
                        zeros = "0";
                        break;
                    case 5:
                        zeros = "";
                        break;
                }
                string orderId = year + month + day + zeros + id.ToString(CultureInfo.InvariantCulture);
                return orderId;
            }
            return year + month + day + "00001";
        }
        #endregion

        #endregion
    }
}