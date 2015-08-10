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
using EPMS.Web.ModelMappers.Inventory.RFI;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.IRF;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using ItemRelease = EPMS.Web.Models.ItemRelease;
using ItemReleaseDetail = EPMS.Web.Models.ItemReleaseDetail;
using ItemWarehouse = EPMS.Web.Models.ItemWarehouse;
using RFI = EPMS.Web.Models.RFI;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class ItemReleaseController : BaseController
    {
        #region Private

        private readonly IItemReleaseService itemReleaseService;
        private readonly IOrdersService ordersService;
        private readonly IRFIService rfiService;
        private readonly IAspNetUserService userService;

        #endregion

        #region Constructor
        public ItemReleaseController(IItemReleaseService itemReleaseService, IOrdersService ordersService, IAspNetUserService userService, IRFIService rfiService)
        {
            this.itemReleaseService = itemReleaseService;
            this.ordersService = ordersService;
            this.userService = userService;
            this.rfiService = rfiService;
        }

        #endregion

        #region Public

        #region Index
        // GET List View: Inventory/IRF
        [SiteAuthorize(PermissionKey = "ItemReleaseIndex")]
        public ActionResult Index()
        {
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            ItemReleaseListViewModel viewModel = new ItemReleaseListViewModel
            {
                SearchRequest = new ItemReleaseSearchRequest()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        /// <summary>
        /// For data table DB paging
        /// </summary>
        [HttpPost]
        public ActionResult Index(ItemReleaseSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            if (Session["RoleName"] != null && Session["RoleName"].ToString() == "InventoryManager")
                ViewBag.UserRole = Session["RoleName"].ToString().ToLower();

            searchRequest.Requester = (UserRole)Convert.ToInt32(Session["RoleKey"].ToString()) == UserRole.Employee ? Session["UserID"].ToString() : "Admin";
            ItemReleaseResponse response = itemReleaseService.GetAllItemRelease(searchRequest);
            IEnumerable<ItemRelease> itemReleaseList =
                response.ItemReleases.Any() ?
                response.ItemReleases.Select(x => x.CreateFromServerToClient()) :
                new List<ItemRelease>();
            ItemReleaseListViewModel viewModel = new ItemReleaseListViewModel()
            {
                aaData = itemReleaseList,
                iTotalRecords = Convert.ToInt32(response.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(response.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Details
        // GET Details: Inventory/IRF
        [SiteAuthorize(PermissionKey = "IRFViewComplete,ItemReleaseDetail")]
        public ActionResult Detail(long? id, string from)
        {
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsAllowedCompleteView = userPermissionsSet.Contains("IRFViewComplete");
            ItemReleaseDetailViewModel viewModel = new ItemReleaseDetailViewModel();
            if (id != null)
            {
                var itemRelease = itemReleaseService.FindItemReleaseById((long)id, from);
                if (itemRelease != null)
                {
                    viewModel.ItemRelease = itemRelease.CreateFromServerToClient();
                    viewModel.ItemReleaseDetails = itemRelease.ItemReleaseDetails.Select(x => x.CreateFromServerToClient());
                }
                else
                {
                    viewModel.ItemRelease = new ItemRelease();
                    viewModel.ItemReleaseDetails = new List<ItemReleaseDetail>();
                }
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            ViewBag.From = from;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Detail(ItemReleaseDetailViewModel viewModel)
        {
            viewModel.ItemRelease.RecUpdatedBy = User.Identity.GetUserId();
            viewModel.ItemRelease.RecUpdatedDate = DateTime.Now;
            viewModel.ItemRelease.ManagerId = User.Identity.GetUserId();
            var itemReleaseToUpdate = viewModel.ItemRelease.CreateForStatus();
            if (itemReleaseService.UpdateItemReleaseStatus(itemReleaseToUpdate))
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = Resources.Inventory.IRF.View.IRFView.RecordUpdated,
                    IsUpdated = true
                };
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        #endregion

        #region Create
        /// <summary>
        /// GET: Inventory/IRF
        /// </summary>
        [SiteAuthorize(PermissionKey = "ItemReleaseCreate,ItemReleaseDetail")]
        public ActionResult Create(long? id)
        {
            ItemReleaseCreateViewModel viewModel = new ItemReleaseCreateViewModel();
            IRFCreateResponse response;
            if (id != null)
            {
                response = itemReleaseService.GetCreateResponse((long)id);
                if (response.ItemRelease != null)
                {
                    viewModel.ItemRelease = response.ItemRelease.CreateFromServerToClient();
                    viewModel.ItemReleaseDetails =
                        response.ItemRelease.ItemReleaseDetails.Select(x => x.CreateFromServerToClient()).ToList();
                }
                else
                {
                    viewModel.ItemRelease = new ItemRelease();
                    viewModel.ItemReleaseDetails = new List<ItemReleaseDetail>();
                }
                viewModel.Rfis = response.Rfis.Any() ? response.Rfis.Select(x => x.CreateRfiServerToClientForDropdown()).ToList() : new List<RFI>();
            }
            else
            {
                response = itemReleaseService.GetCreateResponse(0);
                var direction = Resources.Shared.Common.TextDirection;
                viewModel.ItemRelease = new ItemRelease
                {
                    CreatedBy = direction == "ltr" ? Session["UserFullName"].ToString() : Session["UserFullNameA"].ToString(),
                    FormNumber = Utility.GenerateFormNumber("IR", response.LastFormNumber)
                };
                viewModel.ItemReleaseDetails = new List<ItemReleaseDetail>();
            }
            viewModel.Employees = response.Employees.Any() ?
                response.Employees.Select(x => x.CreateForIrfRequesterDropDownList()).ToList() : new List<EmployeeForDropDownList>();
            viewModel.ItemWarehouses = response.ItemWarehouses.Any() ?
                response.ItemWarehouses.Select(x => x.CreateForItemWarehouse()).ToList() : new List<ItemWarehouse>();
            viewModel.ItemVariationDropDownList = response.ItemVariationDropDownList;
            ViewBag.IsIncludeNewJsTree = true;
            return View(viewModel);
        }

        // POST: Inventory/ItemRelease/Create
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(ItemReleaseCreateViewModel viewModel)
        {
            if (viewModel.ItemRelease.ItemReleaseId > 0)
            {
                // Update
                viewModel.ItemRelease.RecUpdatedBy = User.Identity.GetUserId();
                viewModel.ItemRelease.RecUpdatedDate = DateTime.Now;
                var itemReleaseToUpdate = viewModel.ItemRelease.CreateFromClientToServer();
                itemReleaseToUpdate.QuantityReleased = 0;
                itemReleaseToUpdate.Status = 3;
                foreach (var itemReleaseDetail in viewModel.ItemReleaseDetails)
                {
                    itemReleaseToUpdate.QuantityReleased += itemReleaseDetail.ItemQty;
                    if (itemReleaseDetail.IRFDetailId > 0)
                    {
                        itemReleaseDetail.RecUpdatedBy = User.Identity.GetUserId();
                        itemReleaseDetail.RecUpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        itemReleaseDetail.RecCreatedBy = User.Identity.GetUserId();
                        itemReleaseDetail.RecCreatedDate = DateTime.Now;
                        itemReleaseDetail.RecUpdatedBy = User.Identity.GetUserId();
                        itemReleaseDetail.RecUpdatedDate = DateTime.Now;
                    }
                }
                var itemReleaseDetailsToUpdate = viewModel.ItemReleaseDetails.Select(x => x.CreateFromClientToServer()).ToList();
                if (itemReleaseService.UpdateItemRelease(itemReleaseToUpdate, itemReleaseDetailsToUpdate))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.Inventory.IRF.AddEdit.IRFCreate.RecordUpdated,
                        IsUpdated = true
                    };
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Add
                viewModel.ItemRelease.Status = 3;
                viewModel.ItemRelease.RecCreatedBy = User.Identity.GetUserId();
                viewModel.ItemRelease.RecCreatedDate = DateTime.Now;
                viewModel.ItemRelease.RecUpdatedBy = User.Identity.GetUserId();
                viewModel.ItemRelease.RecUpdatedDate = DateTime.Now;
                var itemReleaseToAdd = viewModel.ItemRelease.CreateFromClientToServer();
                itemReleaseToAdd.QuantityReleased = 0;
                foreach (var itemReleaseDetail in viewModel.ItemReleaseDetails)
                {
                    itemReleaseToAdd.QuantityReleased += itemReleaseDetail.ItemQty;
                    itemReleaseDetail.RecCreatedBy = User.Identity.GetUserId();
                    itemReleaseDetail.RecCreatedDate = DateTime.Now;
                    itemReleaseDetail.RecUpdatedBy = User.Identity.GetUserId();
                    itemReleaseDetail.RecUpdatedDate = DateTime.Now;
                }
                var itemReleaseDetailsToAdd = viewModel.ItemReleaseDetails.Select(x => x.CreateFromClientToServer()).ToList();
                if (itemReleaseService.AddItemRelease(itemReleaseToAdd, itemReleaseDetailsToAdd))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.Inventory.IRF.AddEdit.IRFCreate.RecordAdded,
                        IsUpdated = true
                    };
                    return RedirectToAction("Index");
                }
            }
            return View(viewModel);
        }

        #endregion

        #region History
        [SiteAuthorize(PermissionKey = "IRFHistory")]
        public ActionResult History(long? id)
        {
            IrfHistoryResponse response = itemReleaseService.GetIrfHistoryData(id);
            IrfHistoryViewModel viewModel = new IrfHistoryViewModel
            {
                Irfs = response.Irfs.Any() ? response.Irfs.Select(x => x.CreateFromServerToClient()).ToList() : new List<ItemRelease>(),
                RecentIrf = response.RecentIrf != null ? response.RecentIrf.CreateFromServerToClient() : new ItemRelease(),
                IrfItems = response.IrfItems.Any() ? response.IrfItems.Select(x => x.CreateFromServerToClient()).ToList() : new List<ItemReleaseDetail>()
            };
            viewModel.RecentIrf.RequesterName = response.RequesterNameEn;
            viewModel.RecentIrf.RequesterNameAr = response.RequesterNameAr;
            viewModel.RecentIrf.ManagerName = response.ManagerNameEn;
            viewModel.RecentIrf.ManagerNameAr = response.ManagerNameAr;
            return View(viewModel);
        }
        // POST: Inventory/ItemRelease/History
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult History(IrfHistoryViewModel viewModel)
        {
            viewModel.RecentIrf.RecUpdatedBy = User.Identity.GetUserId();
            viewModel.RecentIrf.RecUpdatedDate = DateTime.Now;
            viewModel.RecentIrf.ManagerId = User.Identity.GetUserId();
            var itemReleaseToUpdate = viewModel.RecentIrf.CreateForStatus();
            if (itemReleaseService.UpdateItemReleaseStatus(itemReleaseToUpdate))
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = Resources.Inventory.IRF.View.IRFView.RecordUpdated,
                    IsUpdated = true
                };
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        #endregion

        #endregion

        #region Get Customer RFIs

        [HttpGet]
        public JsonResult GetRequesterRfis(string requesterId)
        {
            var rfis = rfiService.GetRfiByRequesterId(requesterId);
            IList<RFI> requesterRfis = rfis.Select(x => x.CreateRfiServerToClientForDropdown()).ToList();
            return Json(requesterRfis, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}