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
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.TIR;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using TIR = EPMS.Web.Models.TIR;
using TIRItem = EPMS.Web.Models.TIRItem;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class TransferItemController : BaseController
    {
        private readonly ITIRService tirService;
        private readonly IItemVariationService itemVariationService;

        #region Construcor
        public TransferItemController(ITIRService tirService, IItemVariationService itemVariationService)
        {
            this.tirService = tirService;
            this.itemVariationService = itemVariationService;
        }

        #endregion

        // GET: Inventory/TransferItem
        [SiteAuthorize(PermissionKey = "TIRListView")]
        public ActionResult Index()
        {
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsAllowedCompleteLV = userPermissionsSet.Contains("TIRDetailUpdate");
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
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            searchRequest.CompleteAccess = userPermissionsSet.Contains("TIRDetailUpdate");
            searchRequest.Direction = Resources.Shared.Common.TextDirection;
            TIRListResponse response = tirService.GetAllTirs(searchRequest);
            IEnumerable<Models.TIR> transferItemList =
                response.TirItems.Select(x => x.CreateFromServerToClient());
            TransferItemListViewModel viewModel = new TransferItemListViewModel
            {
                aaData = transferItemList,
                iTotalRecords = Convert.ToInt32(response.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(response.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [SiteAuthorize(PermissionKey = "TIRDetailUpdate,TIRDetail")]
        public ActionResult Detail(long? id, string from)
        {
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsAllowedCompleteView = userPermissionsSet.Contains("TIRDetailUpdate");
            TransferItemCreateViewModel viewModel = new TransferItemCreateViewModel();
            if (id != null)
            {
                var tir = tirService.FindTirById((long) id,from);
                viewModel.Tir = tir.CreateFromServerToClient();
                viewModel.TirItems = tir.TIRItems.Select(x => x.CreateFromServerToClient()).ToList();
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        public ActionResult Detail(TransferItemCreateViewModel viewModel)
        {
            var notesE = viewModel.Tir.NotesE;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = viewModel.Tir.NotesA;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            TransferItemStatus itemStatus = new TransferItemStatus
            {
                Id = viewModel.Tir.Id,
                NotesEn = notesE,
                NotesAr = notesA,
                Status = viewModel.Tir.Status,
                ManagerId = User.Identity.GetUserId()
            };
            if (tirService.UpdateTirStatus(itemStatus))
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = Resources.Inventory.TIR.TIRDetail.RecordUpdated,
                    IsUpdated = true
                };
                return RedirectToAction("Detail", new { id = viewModel.Tir.Id});
            }
            return View(viewModel);
        }

        // GET: Inventory/Dif/Create
        [SiteAuthorize(PermissionKey = "TIRCreate,TIRDetail")]
        public ActionResult Create(long? id)
        {
            var tirResponse = tirService.LoadTirResponseData(id);
            TransferItemCreateViewModel viewModel = new TransferItemCreateViewModel();
            if (tirResponse.Tir != null)
            {
                viewModel.Tir = tirResponse.Tir.CreateFromServerToClient();
                viewModel.TirItems = tirResponse.Tir.TIRItems.Select(x=>x.CreateFromServerToClient()).ToList();
            }
            else
            {
                viewModel.Tir = new Models.TIR
                {
                    FormNumber = "101010",
                    RequesterName = Session["UserFullName"].ToString()
                };
                viewModel.TirItems = new List<Models.TIRItem>();
            }
            viewModel.ItemVariationDropDownList = tirResponse.ItemVariationDropDownList;
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
                        Message = Resources.Inventory.TIR.TIRCreate.RecordUpdated,
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
                        Message = Resources.Inventory.TIR.TIRCreate.RecordAdded,
                        IsSaved = true
                    };
                }

                var tirToBeSaved = viewModel.CreateFromClientToServer();
                if (tirService.SavePO(tirToBeSaved))
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
        [SiteAuthorize(PermissionKey = "TIRHistory")]
        public ActionResult History(long? id)
        {
            TirHistoryResponse response = tirService.GetTirHistoryData(id);
            TirHistoryViewModel viewModel = new TirHistoryViewModel
            {
                Tirs = response.Tirs != null ? response.Tirs.Select(x=>x.CreateFromServerToClient()).ToList() : new List<TIR>(),
                RecentTir = response.RecentTir != null ? response.RecentTir.CreateFromServerToClient() : new TIR(),
                TirItems = response.TirItems.Any() ? response.TirItems.Select(x => x.CreateFromServerToClient()).ToList() : new List<TIRItem>()
            };
            if (response.RecentTir != null)
            {
                viewModel.RecentTir.RequesterName = response.RequesterNameEn;
                viewModel.RecentTir.RequesterNameAr = response.RequesterNameAr;
                viewModel.RecentTir.ManagerName = response.ManagerNameEn;
                viewModel.RecentTir.ManagerNameAr = response.ManagerNameAr;
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        public ActionResult History(TirHistoryViewModel viewModel)
        {
            var notesE = viewModel.RecentTir.NotesE;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = viewModel.RecentTir.NotesA;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            TransferItemStatus itemStatus = new TransferItemStatus
            {
                Id = viewModel.RecentTir.Id,
                NotesEn = notesE,
                NotesAr = notesA,
                Status = viewModel.RecentTir.Status,
                ManagerId = User.Identity.GetUserId()
            };
            if (tirService.UpdateTirStatus(itemStatus))
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = Resources.Inventory.TIR.TIRDetail.RecordUpdated,
                    IsUpdated = true
                };
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}