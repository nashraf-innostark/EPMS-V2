using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.TIR;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class TransferItemController : BaseController
    {
        #region Private

        private readonly ITIRService tirService;
        private readonly IItemVariationService itemVariationService;

        #endregion

        #region Construcor
        public TransferItemController(ITIRService tirService, IItemVariationService itemVariationService)
        {
            this.tirService = tirService;
            this.itemVariationService = itemVariationService;
        }

        #endregion

        #region Public

        #region Index
        // GET: Inventory/TransferItem
        [SiteAuthorize(PermissionKey = "TIRListView")]
        public ActionResult Index()
        {
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            TransferItemListViewModel viewModel = new TransferItemListViewModel
            {
                SearchRequest = new TransferItemSearchRequest()
            };

            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        /// <summary>
        /// For data table DB paging
        /// </summary>
        [HttpPost]
        public ActionResult Index(TransferItemSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            searchRequest.Requester = (UserRole)Convert.ToInt32(Session["RoleKey"].ToString()) == UserRole.Employee ? Session["UserID"].ToString() : "Admin";
            searchRequest.Direction = EPMS.WebModels.Resources.Shared.Common.TextDirection;
            TIRListResponse response = tirService.GetAllTirs(searchRequest);
            IEnumerable<WebModels.WebsiteModels.TIR> transferItemList =
                response.TirItems.Any() ? response.TirItems.Select(x => x.CreateFromServerToClient()) : new List<WebModels.WebsiteModels.TIR>();
            TransferItemListViewModel viewModel = new TransferItemListViewModel
            {
                aaData = transferItemList,
                iTotalRecords = Convert.ToInt32(response.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(response.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detail
        [SiteAuthorize(PermissionKey = "TIRDetailUpdate,TIRDetail")]
        public ActionResult Detail(long? id, string from)
        {
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsAllowedCompleteView = userPermissionsSet.Contains("TIRDetailUpdate");
            TransferItemCreateViewModel viewModel = new TransferItemCreateViewModel();
            if (id != null)
            {
                var tir = tirService.FindTirById((long)id, from);
                if (tir != null)
                {
                    viewModel.Tir = tir.CreateFromServerToClient();
                    viewModel.TirItems = tir.TIRItems.Select(x => x.CreateFromServerToClient()).ToList();
                }
                else
                {
                    viewModel.Tir = new WebModels.WebsiteModels.TIR();
                    viewModel.TirItems = new List<WebModels.WebsiteModels.TIRItem>();
                }
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            ViewBag.From = from;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        public ActionResult Detail(TransferItemCreateViewModel viewModel)
        {
            viewModel.Tir.RecUpdatedBy = User.Identity.GetUserId();
            viewModel.Tir.RecUpdatedDate = DateTime.Now;
            viewModel.Tir.ManagerId = User.Identity.GetUserId();
            var tirToUpdate = viewModel.Tir.CreateForStatus();
            if (tirService.UpdateTirStatus(tirToUpdate))
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = EPMS.WebModels.Resources.Inventory.TIR.TIRDetail.RecordUpdated,
                    IsUpdated = true
                };
                return RedirectToAction("Index");
                //return RedirectToAction("Detail", new { id = viewModel.Tir.Id});
            }
            return View(viewModel);
        }
        #endregion

        #region Create
        // GET: Inventory/Dif/Create
        [SiteAuthorize(PermissionKey = "TIRCreate,TIRDetail")]
        public ActionResult Create(long? id)
        {
            var direction = EPMS.WebModels.Resources.Shared.Common.TextDirection;
            var tirResponse = tirService.LoadTirResponseData(id);
            TransferItemCreateViewModel viewModel = new TransferItemCreateViewModel();
            if (tirResponse.Tir != null)
            {
                viewModel.Tir = tirResponse.Tir.CreateFromServerToClient();
                viewModel.TirItems = tirResponse.Tir.TIRItems.Any() ?
                    tirResponse.Tir.TIRItems.Select(x => x.CreateFromServerToClient()).ToList() : new List<WebModels.WebsiteModels.TIRItem>();
            }
            else
            {
                viewModel.Tir = new WebModels.WebsiteModels.TIR
                {
                    FormNumber = Utility.GenerateFormNumber("TI", tirResponse.LastFormNumber),
                    RequesterName = direction == "ltr" ? Session["UserFullName"].ToString() : Session["UserFullNameA"].ToString()
                };
                viewModel.TirItems = new List<WebModels.WebsiteModels.TIRItem>();
            }
            viewModel.Warehouses = tirResponse.Warehouses.Select(x => x.CreateDDL()).ToList();
            viewModel.ItemVariationDropDownList = tirResponse.ItemVariationDropDownList;
            ViewBag.IsIncludeNewJsTree = true;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        public ActionResult Create(TransferItemCreateViewModel viewModel)
        {
            try
            {
                if (viewModel.Tir.Id > 0)
                {
                    // Update
                    viewModel.Tir.Status = 3;
                    viewModel.Tir.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.Tir.RecUpdatedDate = DateTime.Now;

                    TempData["message"] = new MessageViewModel
                    {
                        Message = EPMS.WebModels.Resources.Inventory.TIR.TIRCreate.RecordUpdated,
                        IsUpdated = true
                    };
                }
                else
                {
                    // Add
                    viewModel.Tir.Status = 3;
                    viewModel.Tir.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.Tir.RecCreatedDate = DateTime.Now;

                    viewModel.Tir.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.Tir.RecUpdatedDate = DateTime.Now;
                    TempData["message"] = new MessageViewModel
                    {
                        Message = EPMS.WebModels.Resources.Inventory.TIR.TIRCreate.RecordAdded,
                        IsSaved = true
                    };
                }

                var tirToBeSaved = viewModel.CreateFromClientToServer();
                if (tirService.SaveTIR(tirToBeSaved))
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
        #endregion

        #region History
        [SiteAuthorize(PermissionKey = "TIRHistory")]
        public ActionResult History(long? id)
        {
            TirHistoryResponse response = tirService.GetTirHistoryData(id);
            TirHistoryViewModel viewModel = new TirHistoryViewModel
            {
                Tirs = response.Tirs.Any() ? response.Tirs.Select(x => x.CreateFromServerToClient()).ToList() : new List<WebModels.WebsiteModels.TIR>(),
                RecentTir = response.RecentTir != null ? response.RecentTir.CreateFromServerToClient() : new WebModels.WebsiteModels.TIR(),
                TirItems = response.TirItems.Any() ? response.TirItems.Select(x => x.CreateFromServerToClient()).ToList() : new List<WebModels.WebsiteModels.TIRItem>()
            };
            viewModel.RecentTir.RequesterName = response.RequesterNameEn;
            viewModel.RecentTir.RequesterNameAr = response.RequesterNameAr;
            viewModel.RecentTir.ManagerName = response.ManagerNameEn;
            viewModel.RecentTir.ManagerNameAr = response.ManagerNameAr;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        public ActionResult History(TirHistoryViewModel viewModel)
        {
            viewModel.RecentTir.RecUpdatedBy = User.Identity.GetUserId();
            viewModel.RecentTir.RecUpdatedDate = DateTime.Now;
            viewModel.RecentTir.ManagerId = User.Identity.GetUserId();
            var tirToUpdate = viewModel.RecentTir.CreateForStatus();
            if (tirService.UpdateTirStatus(tirToUpdate))
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = EPMS.WebModels.Resources.Inventory.TIR.TIRDetail.RecordUpdated,
                    IsUpdated = true
                };
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
        #endregion

        #endregion
    }
}