using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
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
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsManager = userPermissionsSet.Contains("PODetailsUpdation");
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
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            searchRequest.IsManager = userPermissionsSet.Contains("PODetailsUpdation");
            searchRequest.Direction = Resources.Shared.Common.TextDirection;
            PurchaseOrderListResponse response = orderService.GetAllPoS(searchRequest);
            IEnumerable<PurchaseOrder> ordersList =
                response.Orders.Select(x => x.CreateFromServerToClient());
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
                var purchaseOrder = orderService.FindPoById((long)id, from);
                viewModel.PurchaseOrder = purchaseOrder.CreateFromServerToClient();
                viewModel.OrderItems = purchaseOrder.PurchaseOrderItems.Select(x => x.CreateFromServerToClient()).ToList();
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        public ActionResult Details(PurchaseOrderDetailsViewModel viewModel)
        {
            var notesE = viewModel.PurchaseOrder.NotesE;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            viewModel.PurchaseOrder.NotesE = notesE;
            var notesA = viewModel.PurchaseOrder.NotesA;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            viewModel.PurchaseOrder.NotesA = notesA;
            viewModel.PurchaseOrder.ManagerId = User.Identity.GetUserId();
            viewModel.PurchaseOrder.RecUpdatedBy = User.Identity.GetUserId();
            viewModel.PurchaseOrder.RecUpdatedDate = DateTime.Now;
            var purchaseOrderToUpdate = viewModel.PurchaseOrder.CreateFromClientToServer();
            if (orderService.UpdatePO(purchaseOrderToUpdate))
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
                viewModel.Order = new PurchaseOrder
                {
                    FormNumber = "101010",
                    RequesterName = Session["UserFullName"].ToString()
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
    }
}