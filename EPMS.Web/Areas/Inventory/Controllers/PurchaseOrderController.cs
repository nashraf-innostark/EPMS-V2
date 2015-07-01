using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.PurchaseOrder;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class PurchaseOrderController : BaseController
    {
        private readonly IPurchaseOrderService orderService;

        #region Construcor
        public PurchaseOrderController(IPurchaseOrderService orderService)
        {
            this.orderService = orderService;
        }

        #endregion
        
        /// <summary>
        /// GET: Inventory/PurchaseOrder
        /// </summary>
        [SiteAuthorize(PermissionKey = "POIndex")]
        public ActionResult Index()
        {
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower(); 
            PurchaseOrderListViewModel viewModel = new PurchaseOrderListViewModel
            {
                SearchRequest = new PurchaseOrderSearchRequest()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        /// <summary>
        /// For data table DB paging
        /// </summary>
        [HttpPost]
        public ActionResult Index(PurchaseOrderSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            searchRequest.Requester = (UserRole)Convert.ToInt32(Session["RoleKey"].ToString()) == UserRole.Employee ? Session["UserID"].ToString() : "Admin";
            searchRequest.Direction = Resources.Shared.Common.TextDirection;
            PurchaseOrderListResponse response = orderService.GetAllPoS(searchRequest);
            IEnumerable<PurchaseOrder> ordersList = response.Orders.Any() ?
                response.Orders.Select(x => x.CreateFromServerToClient()) : new List<PurchaseOrder>();
            PurchaseOrderListViewModel viewModel = new PurchaseOrderListViewModel
            {
                aaData = ordersList,
                iTotalRecords = Convert.ToInt32(response.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(response.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GET: Inventory/PurchaseOrder
        /// </summary>
        [SiteAuthorize(PermissionKey = "PODetailsUpdation,PODetails")]
        public ActionResult Details(long? id, string from)
        {
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsManager = userPermissionsSet.Contains("PODetailsUpdation");
            PurchaseOrderDetailsViewModel viewModel = new PurchaseOrderDetailsViewModel();
            if (id != null)
            {
                var response = orderService.GetPoDetailResponse((long)id, from);
                if (response.PurchaseOrder != null)
                {
                    viewModel.PurchaseOrder = response.PurchaseOrder.CreateFromServerToClient();
                    viewModel.OrderItems = response.OrderItems.Select(x => x.CreateFromServerToClient()).ToList();
                }
                else
                {
                    viewModel.PurchaseOrder = new PurchaseOrder();
                    viewModel.OrderItems = new List<PurchaseOrderItem>();
                }
                viewModel.Vendors = response.Vendors.Any() ? response.Vendors.Select(x => x.CreateFromServerToClient()) : new List<Vendor>();
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            ViewBag.From = from;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        public ActionResult Details(PurchaseOrderDetailsViewModel viewModel)
        {
            viewModel.PurchaseOrder.ManagerId = User.Identity.GetUserId();
            viewModel.PurchaseOrder.RecUpdatedBy = User.Identity.GetUserId();
            viewModel.PurchaseOrder.RecUpdatedDate = DateTime.Now;
            var purchaseOrderToUpdate = viewModel.CreateFromClientToServer();
            if (orderService.SavePO(purchaseOrderToUpdate))
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = "PO Status Updated",
                    IsUpdated = true
                };
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        /// <summary>
        /// Create: Inventory/PurchaseOrder
        /// </summary>
        [SiteAuthorize(PermissionKey = "POCreate,PODetails")]
        public ActionResult Create(long? id)
        {
            var response = orderService.GetPoResponseData(id);
            PurchaseOrderCreateViewModel viewModel = new PurchaseOrderCreateViewModel();
            if (response.Order != null)
            {
                viewModel.Order = response.Order.CreateFromServerToClient();
                viewModel.PoItems = response.OrderItems.Select(x => x.CreateFromServerToClient()).ToList();
            }
            else
            {
                var direction = Resources.Shared.Common.TextDirection;
                viewModel.Order = new PurchaseOrder
                {
                    FormNumber = "101010",
                    RequesterName = direction == "ltr" ? Session["UserFullName"].ToString() : Session["UserFullNameA"].ToString()
                };
                viewModel.PoItems = new List<PurchaseOrderItem>();
            }
            viewModel.ItemVariationDropDownList = response.ItemVariationDropDownList;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        public ActionResult Create(PurchaseOrderCreateViewModel viewModel)
        {
            try
            {
                if (viewModel.Order.PurchaseOrderId > 0)
                {
                    // Update
                    viewModel.Order.Status = 3;
                    viewModel.Order.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.Order.RecUpdatedDate = DateTime.Now;

                    TempData["message"] = new MessageViewModel
                    {
                        Message = "PO Updated",
                        IsUpdated = true
                    };
                }
                else
                {
                    // Add
                    viewModel.Order.Status = 3;
                    viewModel.Order.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.Order.RecCreatedDate = DateTime.Now;

                    viewModel.Order.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.Order.RecUpdatedDate = DateTime.Now;
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "PO Added",
                        IsSaved = true
                    };
                }

                var poToBeSaved = viewModel.CreateFromClientToServer();
                if (orderService.SavePO(poToBeSaved))
                {
                    //success
                    return RedirectToAction("Index");
                }
                //failed to save
                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = e.Message,
                    IsSaved = true
                };
                return View(viewModel);
            }
        }

        [SiteAuthorize(PermissionKey = "PODetailsUpdation")]
        public ActionResult History(long? id)
        {
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsManager = userPermissionsSet.Contains("PODetailsUpdation");
            PoHistoryResponse response = orderService.GetPoHistoryData(id);
            POHistoryViewModel viewModel = new POHistoryViewModel
            {
                PurchaseOrders = response.PurchaseOrders.Any() ? response.PurchaseOrders.Select(x => x.CreateFromServerToClient()).ToList() : new List<PurchaseOrder>(),
                PurchaseOrdersItems = response.PoItems.Any() ? response.PoItems.Select(x => x.CreateFromServerToClient()).ToList() : new List<PurchaseOrderItem>(),
                RecentPo = (response.RecentPo != null && response.RecentPo.PurchaseOrderId > 0) ? response.RecentPo.CreateFromServerToClient() : new PurchaseOrder(),
                Vendors = response.Vendors.Any() ? response.Vendors.Select(x=>x.CreateFromServerToClient()).ToList() : new List<Vendor>()
            };
            viewModel.RecentPo.RequesterName = response.RequesterNameEn;
            viewModel.RecentPo.RequesterNameAr = response.RequesterNameAr;
            viewModel.RecentPo.ManagerName = response.ManagerNameEn;
            viewModel.RecentPo.ManagerNameAr = response.ManagerNameAr;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        public ActionResult History(POHistoryViewModel viewModel)
        {
            // Update
            viewModel.RecentPo.ManagerId = User.Identity.GetUserId();
            viewModel.RecentPo.RecUpdatedBy = User.Identity.GetUserId();
            viewModel.RecentPo.RecUpdatedDate = DateTime.Now;
            var purchaseOrderToUpdate = viewModel.RecentPo.CreateFromClientToServer(viewModel.PurchaseOrdersItems);
            if (orderService.SavePO(purchaseOrderToUpdate))
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = "PO Status Updated",
                    IsUpdated = true
                };
                return RedirectToAction("Index");
            }
            // Error
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsManager = userPermissionsSet.Contains("PODetailsUpdation");
            return View(viewModel);
        }
    }
}