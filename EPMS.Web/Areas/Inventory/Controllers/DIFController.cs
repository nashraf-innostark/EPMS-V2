using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Models.RequestModels;
using System.Configuration;
using System.Globalization;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Inventory.DIF;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.DIF;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "IS", IsModule = true)]
    public class DIFController : BaseController
    {
        private readonly IDIFService rifService;

        public DIFController(IDIFService rifService)
        {
            this.rifService = rifService;
        }

        // GET: Inventory/Dif
        [SiteAuthorize(PermissionKey = "DIFIndex")]
        public ActionResult Index()
        {
            DifSearchRequest searchRequest = Session["PageMetaData"] as DifSearchRequest;
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            Session["PageMetaData"] = null;

            DifListViewModel viewModel = new DifListViewModel
            {
                SearchRequest = searchRequest ?? new DifSearchRequest()
            };

            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(DifSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            DifListViewModel viewModel = new DifListViewModel();
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            if (Session["RoleName"] != null && Session["RoleName"].ToString() == "Manager")
            {
                searchRequest.Requester = "Admin";
            }
            else
            {
                searchRequest.Requester = Session["UserID"].ToString();
            }
            var requestResponse = rifService.LoadAllDifs(searchRequest);
            var data = requestResponse.Difs.Select(x => x.CreateDifServerToClient());
            var responseData = data as IList<Models.DIF> ?? data.ToList();
            if (responseData.Any())
            {
                viewModel.aaData = responseData;
                viewModel.iTotalRecords = requestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = requestResponse.TotalCount;
                viewModel.sEcho = searchRequest.sEcho;
                //viewModel.sLimit = searchRequest.iDisplayLength;
            }
            else
            {
                viewModel.aaData = Enumerable.Empty<Models.DIF>();
                viewModel.iTotalRecords = requestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = requestResponse.TotalCount;
                viewModel.sEcho = searchRequest.sEcho;
                //viewModel.sLimit = searchRequest.iDisplayLength;
            }
            // Keep Search Request in Session
            Session["PageMetaData"] = searchRequest;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Inventory/Dif/Details/5
        [SiteAuthorize(PermissionKey = "DIFDetails")]
        public ActionResult Details(int id)
        {
            var Difresponse = rifService.LoadDifResponseData(id);
            DIFViewModel rifViewModel = new DIFViewModel();
            if (Difresponse.Dif != null)
            {
                rifViewModel.Dif = Difresponse.Dif.CreateDifServerToClient();
                if (Resources.Shared.Common.TextDirection == "ltr")
                {
                    rifViewModel.Dif.RequesterName = Difresponse.RequesterNameE;
                    rifViewModel.Dif.ManagerName = Difresponse.ManagerNameE;
                }
                else
                {
                    rifViewModel.Dif.RequesterName = Difresponse.RequesterNameA;
                    rifViewModel.Dif.ManagerName = Difresponse.ManagerNameA;
                }
                rifViewModel.DifItem = Difresponse.DifItem.Select(x => x.CreateDifItemDetailsServerToClient()).ToList();
            }
            else
            {
                rifViewModel.Dif = new Models.DIF
                {
                    RequesterName = Session["FullName"].ToString()
                };
                rifViewModel.DifItem = new List<Models.DIFItem>();
            }
            rifViewModel.ItemVariationDropDownList = Difresponse.ItemVariationDropDownList;
            return View(rifViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Details(DIFViewModel rifViewModel)
        {
            try
            {
                rifViewModel.Dif.RecUpdatedBy = User.Identity.GetUserId();
                rifViewModel.Dif.RecUpdatedDate = DateTime.Now;
                rifViewModel.Dif.ManagerId = User.Identity.GetUserId();
                TempData["message"] = new MessageViewModel
                {
                    //Message = Resources.Inventory.DIF.DIF.DIFReplied,
                    IsUpdated = true
                };

                var DifToBeSaved = rifViewModel.CreateDifDetailsClientToServer();
                if (rifService.UpdateDIF(DifToBeSaved))
                {
                    //success
                    return RedirectToAction("Index");
                }
                //failed to save
                return View(); 
            }
            catch (Exception)
            {
                return View(); 
            }
        }

        // GET: Inventory/Dif/Create
        [SiteAuthorize(PermissionKey = "DIFCreate")]
        public ActionResult Create(long? id)
        {
            var Difresponse = rifService.LoadDifResponseData(id);
            DIFViewModel rifViewModel = new DIFViewModel();
            if (Difresponse.Dif != null)
            {
                rifViewModel.Dif = Difresponse.Dif.CreateDifServerToClient();
                rifViewModel.Dif.RequesterName = Resources.Shared.Common.TextDirection == "ltr" ? Difresponse.RequesterNameE : Difresponse.RequesterNameA;
                rifViewModel.DifItem = Difresponse.DifItem.Select(x => x.CreateDifItemServerToClient()).ToList();
            }
            else
            {
                rifViewModel.Dif = new Models.DIF
                {
                    RequesterName = Session["UserFullName"].ToString()
                };
                rifViewModel.DifItem = new List<Models.DIFItem>();
            }
            rifViewModel.ItemVariationDropDownList = Difresponse.ItemVariationDropDownList;
            return View(rifViewModel);
        }

        // POST: Inventory/Dif/Create
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(DIFViewModel rifViewModel)
        {
            try
            {
                if (rifViewModel.Dif.Id > 0)
                {
                    rifViewModel.Dif.RecUpdatedBy = User.Identity.GetUserId();
                    rifViewModel.Dif.RecUpdatedDate = DateTime.Now;

                    TempData["message"] = new MessageViewModel
                    {
                        //Message = Resources.Inventory.DIF.DIF.RFIUpdated,
                        IsUpdated = true
                    };
                }
                else
                {
                    rifViewModel.Dif.RecCreatedBy = User.Identity.GetUserId();
                    rifViewModel.Dif.RecCreatedDate = DateTime.Now;

                    rifViewModel.Dif.RecUpdatedBy = User.Identity.GetUserId();
                    rifViewModel.Dif.RecUpdatedDate = DateTime.Now;
                    TempData["message"] = new MessageViewModel
                    {
                        //Message = Resources.Inventory.DIF.DIF.DIFCreated,
                        IsSaved = true
                    };
                }
                
                var DifToBeSaved = rifViewModel.CreateDifClientToServer();
                if(rifService.SaveDIF(DifToBeSaved))
                {
                    //success
                    return RedirectToAction("Index");
                }
                //failed to save
                return View(); 
            }
            catch
            {
                return View();
            }
        }
        [SiteAuthorize(PermissionKey = "DIFHistory")]
        public ActionResult History()
        {
            DifHistoryResponse response = rifService.GetDifHistoryData();
            DifHistoryViewModel viewModel = new DifHistoryViewModel
            {
                Difs = response.Difs != null ? response.Difs.Select(x => x.CreateDifServerToClient()).ToList() : new List<DIF>(),
                RecentDif = response.RecentDif != null ? response.RecentDif.CreateDifServerToClient() : new DIF(),
                DifItems = response.DifItems.Any() ? response.DifItems.Select(x => x.CreateDifItemDetailsServerToClient()).ToList() : new List<DIFItem>()
            };
            if (response.RecentDif != null)
            {
                viewModel.RecentDif.RequesterName = response.RequesterNameEn;
                viewModel.RecentDif.RequesterNameAr = response.RequesterNameAr;
                viewModel.RecentDif.ManagerName = response.ManagerNameEn;
                viewModel.RecentDif.ManagerNameAr = response.ManagerNameAr;
            }
            return View(viewModel);
        }
    }
}
