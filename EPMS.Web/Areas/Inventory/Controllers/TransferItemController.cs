using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.TIR;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class TransferItemController : BaseController
    {
        private readonly ITIRService tirService;

        #region Construcor
        public TransferItemController(ITIRService tirService)
        {
            this.tirService = tirService;
        }
        #endregion

        // GET: Inventory/TransferItem
        [SiteAuthorize(PermissionKey = "TIRListView")]
        public ActionResult Index()
        {
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            ViewBag.IsAllowedCompleteLV = userPermissionsSet.Contains("TIRCompleteListView");
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
            searchRequest.CompleteAccess = userPermissionsSet.Contains("TIRCompleteListView");
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

        // GET: Inventory/Dif/Create
        //[SiteAuthorize(PermissionKey = "")]
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
                    viewModel.Tir.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.Tir.RecUpdatedDate = DateTime.Now;

                    TempData["message"] = new MessageViewModel
                    {
                        Message = "TIR Updated",
                        IsUpdated = true
                    };
                }
                else
                {
                    viewModel.Tir.Status = 3;
                    viewModel.Tir.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.Tir.RecCreatedDate = DateTime.Now;

                    viewModel.Tir.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.Tir.RecUpdatedDate = DateTime.Now;
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "TIR Created",
                        IsSaved = true
                    };
                }

                var tirToBeSaved = viewModel.CreateFromClientToServer();
                if (tirService.SaveDIF(tirToBeSaved))
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