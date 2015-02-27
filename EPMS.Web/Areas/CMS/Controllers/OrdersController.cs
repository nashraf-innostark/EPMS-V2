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
        // GET: HR/Orders
        [SiteAuthorize(PermissionKey = "OrdersIndex")]
        public ActionResult Index()
        {
            OrdersListViewModel viewModel = new OrdersListViewModel();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var firstOrDefault = result.AspNetRoles.FirstOrDefault();
            if (firstOrDefault != null)
            {
                viewModel.SearchRequest.Role = firstOrDefault.Name;
                viewModel.SearchRequest.CustomerId = result.CustomerId ?? 0;
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(OrdersSearchRequest searchRequest)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var firstOrDefault = result.AspNetRoles.FirstOrDefault();
            if (firstOrDefault != null)
            {
                searchRequest.Role = firstOrDefault.Name;
                searchRequest.CustomerId = result.CustomerId ?? 0;
            }
            searchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
            searchRequest.SearchString = Request["search"];
            OrdersResponse ordersList = null;
            IEnumerable<Order> orders = null;
            //Models.Customer customer = null;
            if (searchRequest.Role != null)
            {
                if (searchRequest.Role == "Admin")
                {
                    searchRequest.CustomerId = 0;
                    ordersList = OrdersService.GetAllOrders(searchRequest);
                    orders = ordersList.Orders.Select(o => o.CreateFromServerToClientLv());
                }
                if (searchRequest.Role == "Customer")
                {
                    searchRequest.CustomerId = searchRequest.CustomerId;
                    ordersList = OrdersService.GetAllOrders(searchRequest);
                    orders = ordersList.Orders.Select(o => o.CreateFromServerToClientLv());
                }
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
        [SiteAuthorize(PermissionKey = "OrderCreate")]
        public ActionResult Create(long? id)
        {
            var direction = Resources.Shared.Common.TextDirection;
            OrdersCreateViewModel viewModel = new OrdersCreateViewModel();
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var role = result.AspNetRoles.FirstOrDefault();
            if (role != null) viewModel.RoleName = role.Name;
            ViewBag.backUrl = Request.UrlReferrer;
            if (id != null)
            {
                viewModel.Orders = OrdersService.GetOrderByOrderId((long)id).CreateFromServerToClient();
                if (viewModel.RoleName == "Customer")
                {
                    viewModel.PageTitle = Resources.CMS.Order.PTCreateUpdate;
                }
                if (viewModel.RoleName == "Admin")
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
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(OrdersCreateViewModel viewModel)
        {
            var direction = Resources.Shared.Common.TextDirection;
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var role = result.AspNetRoles.FirstOrDefault();
            if (role != null) viewModel.RoleName = role.Name;
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
                var customerId = result.CustomerId;
                viewModel.Orders.OrderDate = DateTime.Now;
                viewModel.Orders.CustomerId = customerId ?? 0;
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
            if (viewModel.RoleName == "Admin")
            {
                viewModel.PageTitle = Resources.CMS.Order.PTCreateUpdate;
            }
            if (viewModel.RoleName == "Customer")
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

        #region Get Order Number
        string GetOrderNumber()
        {
            string year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            string month = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture);
            string day = DateTime.Now.Day.ToString(CultureInfo.InvariantCulture);
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