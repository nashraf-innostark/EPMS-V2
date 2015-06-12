using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.ModelMappers;
using System.Configuration;
using System.Globalization;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Inventory.RIF;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.RIF;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "IS", IsModule = true)]
    public class RIFController : BaseController
    {
        private readonly IRIFService rifService;

        public RIFController(IRIFService rifService)
        {
            this.rifService = rifService;
        }

        // GET: Inventory/Rif
        [SiteAuthorize(PermissionKey = "RIFIndex")]
        public ActionResult Index()
        {
            RifSearchRequest searchRequest = Session["PageMetaData"] as RifSearchRequest;
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            Session["PageMetaData"] = null;

            RifListViewModel viewModel = new RifListViewModel
            {
                SearchRequest = searchRequest ?? new RifSearchRequest()
            };

            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(RifSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            RifListViewModel viewModel = new RifListViewModel();
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            if (Session["RoleName"] != null && Session["RoleName"].ToString() == "Manager")
            {
                searchRequest.Requester = "Admin";
            }
            else
            {
                searchRequest.Requester = Session["UserID"].ToString();
            }
            var requestResponse = rifService.LoadAllRifs(searchRequest);
            var data = requestResponse.Rifs.Select(x => x.CreateRifServerToClient());
            var responseData = data as IList<Models.RIF> ?? data.ToList();
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
                viewModel.aaData = Enumerable.Empty<Models.RIF>();
                viewModel.iTotalRecords = requestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = requestResponse.TotalCount;
                viewModel.sEcho = searchRequest.sEcho;
                //viewModel.sLimit = searchRequest.iDisplayLength;
            }
            // Keep Search Request in Session
            Session["PageMetaData"] = searchRequest;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Inventory/Rif/Details/5
        [SiteAuthorize(PermissionKey = "RIFDetails")]
        public ActionResult Details(int id)
        {
            var Rifresponse = rifService.LoadRifResponseData(id, false);
            RIFViewModel rifViewModel = new RIFViewModel();
            if (Rifresponse.Rif != null)
            {
                rifViewModel.Rif = Rifresponse.Rif.CreateRifServerToClient();
                if (Resources.Shared.Common.TextDirection == "ltr")
                {
                    rifViewModel.Rif.RequesterName = Rifresponse.RequesterNameE;
                    rifViewModel.Rif.CustomerName = Rifresponse.CustomerNameE;
                    rifViewModel.Rif.ManagerName = Rifresponse.ManagerNameE;
                }
                else
                {
                    rifViewModel.Rif.RequesterName = Rifresponse.RequesterNameA;
                    rifViewModel.Rif.CustomerName = Rifresponse.CustomerNameA;
                    rifViewModel.Rif.ManagerName = Rifresponse.ManagerNameA;
                }
                rifViewModel.Rif.OrderNo = Rifresponse.OrderNo;
                rifViewModel.RifItem = Rifresponse.RifItem.Select(x => x.CreateRifItemDetailsServerToClient()).ToList();
            }
            else
            {
                rifViewModel.Rif = new Models.RIF
                {
                    RequesterName = Session["FullName"].ToString()
                };
                rifViewModel.RifItem = new List<Models.RIFItem>();
            }
            rifViewModel.ItemVariationDropDownList = Rifresponse.ItemVariationDropDownList;
            return View(rifViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Details(RIFViewModel rifViewModel)
        {
            try
            {
                rifViewModel.Rif.RecUpdatedBy = User.Identity.GetUserId();
                rifViewModel.Rif.RecUpdatedDate = DateTime.Now;
                rifViewModel.Rif.ManagerId = User.Identity.GetUserId();
                TempData["message"] = new MessageViewModel
                {
                    Message = Resources.Inventory.RIF.RIF.RIFReplied,
                    IsUpdated = true
                };

                var RifToBeSaved = rifViewModel.CreateRifDetailsClientToServer();
                if (rifService.UpdateRIF(RifToBeSaved))
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

        // GET: Inventory/Rif/Create
        //[SiteAuthorize(PermissionKey = "RIFCreate")]
        public ActionResult Create(long? id)
        {
            bool loadCustomersAndOrders = CheckHasCustomerModule();
            var Rifresponse = rifService.LoadRifResponseData(id, loadCustomersAndOrders);
            RIFViewModel rifViewModel = new RIFViewModel();
            if (Rifresponse.Rif != null)
            {
                rifViewModel.Rif = Rifresponse.Rif.CreateRifServerToClient();
                rifViewModel.Rif.RequesterName = Resources.Shared.Common.TextDirection == "ltr" ? Rifresponse.RequesterNameE : Rifresponse.RequesterNameA;
                rifViewModel.RifItem = Rifresponse.RifItem.Select(x => x.CreateRifItemServerToClient()).ToList();
            }
            else
            {
                rifViewModel.Rif = new Models.RIF
                {
                    RequesterName = Session["UserFullName"].ToString()
                };
                rifViewModel.RifItem = new List<Models.RIFItem>();
            }
            if (loadCustomersAndOrders)
            {
                rifViewModel.Customers = Rifresponse.Customers.Select(x => x.CreateForDashboard());
                rifViewModel.Orders = Rifresponse.Orders.Select(x => x.CreateForDashboard());
                //set customerId
                if (rifViewModel.Rif.OrderId>0)
                    rifViewModel.Rif.CustomerId = rifViewModel.Orders.FirstOrDefault(x => x.OrderId == rifViewModel.Rif.OrderId).CustomerId;
            }
            rifViewModel.ItemVariationDropDownList = Rifresponse.ItemVariationDropDownList;
            return View(rifViewModel);
        }

        // POST: Inventory/Rif/Create
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(RIFViewModel rifViewModel)
        {
            try
            {
                if (rifViewModel.Rif.RIFId > 0)
                {
                    rifViewModel.Rif.RecUpdatedBy = User.Identity.GetUserId();
                    rifViewModel.Rif.RecUpdatedDate = DateTime.Now;

                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.Inventory.RIF.RIF.RFIUpdated,
                        IsUpdated = true
                    };
                }
                else
                {
                    rifViewModel.Rif.RecCreatedBy = User.Identity.GetUserId();
                    rifViewModel.Rif.RecCreatedDate = DateTime.Now;

                    rifViewModel.Rif.RecUpdatedBy = User.Identity.GetUserId();
                    rifViewModel.Rif.RecUpdatedDate = DateTime.Now;
                    TempData["message"] = new MessageViewModel { Message = Resources.Inventory.RIF.RIF.RIFCreated, IsSaved = true };
                }
                
                var RifToBeSaved = rifViewModel.CreateRifClientToServer();
                if(rifService.SaveRIF(RifToBeSaved))
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
        [SiteAuthorize(PermissionKey = "RIFHistory")]
        public ActionResult History()
        {
            RifHistoryResponse response = rifService.GetRifHistoryData();
            RifHistoryViewModel viewModel = new RifHistoryViewModel
            {
                Rifs = response.Rifs != null ? response.Rifs.Select(x => x.CreateRifServerToClient()).ToList() : new List<RIF>(),
                RecentRif = response.RecentRif != null ? response.RecentRif.CreateRifServerToClient() : new RIF(),
                RifItems = response.RifItems.Any() ? response.RifItems.Select(x => x.CreateRifItemDetailsServerToClient()).ToList() : new List<RIFItem>()
            };
            if (response.RecentRif != null)
            {
                viewModel.RecentRif.RequesterName = response.RequesterNameEn;
                viewModel.RecentRif.RequesterNameAr = response.RequesterNameAr;
                viewModel.RecentRif.ManagerName = response.ManagerNameEn;
                viewModel.RecentRif.ManagerNameAr = response.ManagerNameAr;
            }
            return View(viewModel);
        }
        private bool CheckHasCustomerModule()
        {
            // check license
            var licenseKeyEncrypted = ConfigurationManager.AppSettings["LicenseKey"].ToString(CultureInfo.InvariantCulture);
            string LicenseKey = WebBase.EncryptDecrypt.StringCipher.Decrypt(licenseKeyEncrypted, "123");
            var splitLicenseKey = LicenseKey.Split('|');
            string[] Modules = splitLicenseKey[4].Split(';');
            if (Modules.Contains("CS") || Modules.Contains("Customer Service"))
            {
                ViewBag.HasCustomerModule = true;
                return true;
            }
            else
            {
                ViewBag.HasCustomerModule = false;
                return false;
            }
        }
    }
}
