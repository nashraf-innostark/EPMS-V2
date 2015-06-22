using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.Inventory.RFI;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.IRF;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using ItemRelease = EPMS.Web.Models.ItemRelease;
using ItemReleaseDetail = EPMS.Web.Models.ItemReleaseDetail;
using RFI = EPMS.Web.Models.RFI;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class ItemReleaseController : BaseController
    {
        #region Private

        private readonly IItemReleaseService itemReleaseService;
        private readonly IRFIService RfiService;
        private readonly IOrdersService ordersService;
        private readonly IAspNetUserService userService;

        #endregion

        #region Constructor
        public ItemReleaseController(IItemReleaseService itemReleaseService, IRFIService rfiService, IOrdersService ordersService, IAspNetUserService userService)
        {
            this.itemReleaseService = itemReleaseService;
            RfiService = rfiService;
            this.ordersService = ordersService;
            this.userService = userService;
        }

        #endregion

        #region Public
        // GET List View: Inventory/IRF
        [SiteAuthorize(PermissionKey = "ItemReleaseIndex")]
        public ActionResult Index()
        {
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsAllowedCompleteLV = userPermissionsSet.Contains("IRFViewComplete");
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
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            searchRequest.CompleteAccess = userPermissionsSet.Contains("IRFViewComplete");
            ItemReleaseResponse response = itemReleaseService.GetAllItemRelease(searchRequest);
            IEnumerable<Models.ItemRelease> itemReleaseList =
                response.ItemReleases.Select(x => x.CreateFromServerToClient());
            ItemReleaseListViewModel viewModel = new ItemReleaseListViewModel()
            {
                aaData = itemReleaseList,
                iTotalRecords = Convert.ToInt32(response.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(response.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // GET Details: Inventory/IRF
        [SiteAuthorize(PermissionKey = "IRFViewComplete,ItemReleaseDetail")]
        public ActionResult Detail(long? id, string from)
        {
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsAllowedCompleteView = userPermissionsSet.Contains("IRFViewComplete");
            ItemReleaseDetailViewModel viewModel = new ItemReleaseDetailViewModel();
            if (id != null)
            {
                var itemRelease = itemReleaseService.FindItemReleaseById((long)id,from);
                if (itemRelease != null)
                {
                    viewModel.ItemRelease = itemRelease.CreateFromServerToClient();
                    viewModel.ItemReleaseDetails =
                        itemRelease.ItemReleaseDetails.Select(x => x.CreateFromServerToClient());
                }
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Detail(ItemReleaseDetailViewModel viewModel)
        {
            var notesE = viewModel.ItemRelease.Notes;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = viewModel.ItemRelease.NotesAr;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            ItemReleaseStatus status = new ItemReleaseStatus
            {
                ItemReleaseId = viewModel.ItemRelease.ItemReleaseId,
                Status = viewModel.ItemRelease.Status ?? 1,
                Notes = notesE,
                NotesAr = notesA,
                ManagerId = User.Identity.GetUserId()
            };
            if (itemReleaseService.UpdateItemReleaseStatus(status))
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
                viewModel.ItemRelease = response.ItemRelease.CreateFromServerToClient();
                viewModel.ItemReleaseDetails = response.ItemRelease.ItemReleaseDetails.Select(x => x.CreateFromServerToClient()).ToList();
                viewModel.Rfis = response.Rfis.Select(x => x.CreateRfiServerToClientForDropdown()).ToList();
            }
            else
            {
                response = itemReleaseService.GetCreateResponse(0);
                viewModel.ItemRelease = new ItemRelease();
                viewModel.ItemReleaseDetails = new List<ItemReleaseDetail>();
                if (Session["RoleName"] != null)
                {
                    if (Session["RoleName"].ToString() != "Admin")
                    {
                        var employee = userService.FindById(Session["UserID"].ToString()).Employee;
                        var direction = Resources.Shared.Common.TextDirection;
                        if (direction == "ltr")
                        {
                            viewModel.ItemRelease.CreatedBy = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE;
                        }
                        else
                        {
                            viewModel.ItemRelease.CreatedBy = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA;
                        }
                    }
                    else
                    {
                        viewModel.ItemRelease.CreatedBy = "Admin";
                    }
                }
                viewModel.ItemRelease.FormNumber = "010101";
            }
            //Session["RoleName"];
            viewModel.Customers = response.Customers.Select(x => x.CreateFromServerToClient()).ToList();
            viewModel.ItemVariationDropDownList = response.ItemVariationDropDownList;
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
            return View();
        }
        [SiteAuthorize(PermissionKey = "IRFHistory")]
        public ActionResult History(long? id)
        {
            IrfHistoryResponse response = itemReleaseService.GetIrfHistoryData(id);
            IrfHistoryViewModel viewModel = new IrfHistoryViewModel
            {
                Irfs = response.Irfs != null ? response.Irfs.Select(x => x.CreateFromServerToClient()).ToList() : new List<ItemRelease>(),
                RecentIrf = response.RecentIrf != null ? response.RecentIrf.CreateFromServerToClient() : new ItemRelease(),
                IrfItems = response.IrfItems.Any() ? response.IrfItems.Select(x => x.CreateFromServerToClient()).ToList() : new List<ItemReleaseDetail>()
            };
            if (response.RecentIrf != null)
            {
                viewModel.RecentIrf.RequesterName = response.RequesterNameEn;
                viewModel.RecentIrf.RequesterNameAr = response.RequesterNameAr;
                viewModel.RecentIrf.ManagerName = response.ManagerNameEn;
                viewModel.RecentIrf.ManagerNameAr = response.ManagerNameAr;
            }
            return View(viewModel);
        }
        // POST: Inventory/ItemRelease/History
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult History(IrfHistoryViewModel viewModel)
        {
            var notesE = viewModel.RecentIrf.Notes;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = viewModel.RecentIrf.NotesAr;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            ItemReleaseStatus status = new ItemReleaseStatus
            {
                ItemReleaseId = viewModel.RecentIrf.ItemReleaseId,
                Status = viewModel.RecentIrf.Status ?? 1,
                Notes = notesE,
                NotesAr = notesA,
                ManagerId = User.Identity.GetUserId()
            };
            if (itemReleaseService.UpdateItemReleaseStatus(status))
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

        #region Get Customer RFIs

        [HttpGet]
        public JsonResult GetCustomerRfis(long customerId)
        {
            var customerOrders = ordersService.GetOrdersByCustomerIdWithRfis(customerId);
            IList<RFI> customerRfis = new List<RFI>();
            foreach (var customerOrder in customerOrders)
            {
                if (customerOrder.RFIs.Any())
                {
                    foreach (var rfI in customerOrder.RFIs)
                    {
                        customerRfis.Add(rfI.CreateRfiServerToClientForDropdown());
                    }
                }
            }
            return Json(customerRfis, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get ItemWarehouse

        public JsonResult GetItemWarehouse(long itemVariationId)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}