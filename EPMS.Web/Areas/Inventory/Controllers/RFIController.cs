﻿using System;
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
using EPMS.Web.ModelMappers.Inventory.RFI;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.RFI;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using RFI = EPMS.Web.Models.RFI;
using RFIItem = EPMS.Web.Models.RFIItem;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    //[SiteAuthorize(PermissionKey = "IS", IsModule = true)]
    public class RFIController : BaseController
    {
        private readonly IRFIService rfiService;

        public RFIController(IRFIService rfiService)
        {
            this.rfiService = rfiService;
        }

        // GET: Inventory/RFI
        [SiteAuthorize(PermissionKey = "RFIIndex")]
        public ActionResult Index()
        {
            RfiSearchRequest searchRequest = Session["PageMetaData"] as RfiSearchRequest;
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            Session["PageMetaData"] = null;

            RfiListViewModel viewModel = new RfiListViewModel
            {
                SearchRequest = searchRequest ?? new RfiSearchRequest()
            };

            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(RfiSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            RfiListViewModel viewModel = new RfiListViewModel();
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            if (Session["RoleName"] != null && Session["RoleName"].ToString() == "Manager")
            {
                searchRequest.Requester = "Admin";
            }
            else
            {
                searchRequest.Requester = Session["UserID"].ToString();
            }
            var requestResponse = rfiService.LoadAllRfis(searchRequest);
            var data = requestResponse.Rfis.Select(x => x.CreateRfiServerToClient());
            var responseData = data as IList<RFI> ?? data.ToList();
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
                viewModel.aaData = Enumerable.Empty<RFI>();
                viewModel.iTotalRecords = requestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = requestResponse.TotalCount;
                viewModel.sEcho = searchRequest.sEcho;
                //viewModel.sLimit = searchRequest.iDisplayLength;
            }
            // Keep Search Request in Session
            Session["PageMetaData"] = searchRequest;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Inventory/RFI/Details/5
        [SiteAuthorize(PermissionKey = "RFIDetails")]
        public ActionResult Details(int id, string from)
        {
            var rfiresponse = rfiService.LoadRfiResponseData(id, false, from);
            RFIViewModel rfiViewModel = new RFIViewModel();
            if (rfiresponse.Rfi != null)
            {
                rfiViewModel.Rfi = rfiresponse.Rfi.CreateRfiServerToClient();
                if (Resources.Shared.Common.TextDirection == "ltr")
                {
                    rfiViewModel.Rfi.RequesterName = rfiresponse.RequesterNameE;
                    rfiViewModel.Rfi.CustomerName = rfiresponse.CustomerNameE;
                    rfiViewModel.Rfi.ManagerName = rfiresponse.ManagerNameE;
                }
                else
                {
                    rfiViewModel.Rfi.RequesterName = rfiresponse.RequesterNameA;
                    rfiViewModel.Rfi.CustomerName = rfiresponse.CustomerNameA;
                    rfiViewModel.Rfi.ManagerName = rfiresponse.ManagerNameA;
                }
                rfiViewModel.Rfi.OrderNo = rfiresponse.OrderNo;
                rfiViewModel.RfiItem = rfiresponse.RfiItem.Select(x => x.CreateRfiItemDetailsServerToClient()).ToList();
            }
            else
            {
                rfiViewModel.Rfi = new RFI
                {
                    RequesterName = Session["FullName"].ToString()
                };
                rfiViewModel.RfiItem = new List<RFIItem>();
            }
            rfiViewModel.ItemVariationDropDownList = rfiresponse.ItemVariationDropDownList;
            return View(rfiViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Details(RFIViewModel rfiViewModel)
        {
            try
            {
                rfiViewModel.Rfi.RecUpdatedBy = User.Identity.GetUserId();
                rfiViewModel.Rfi.RecUpdatedDate = DateTime.Now;
                rfiViewModel.Rfi.ManagerId = User.Identity.GetUserId();

                TempData["message"] = new MessageViewModel
                {
                    Message = Resources.RFI.RFI.RFIReplied,
                    IsUpdated = true
                };

                var rfiToBeSaved = rfiViewModel.CreateRfiDetailsClientToServer();
                if (rfiService.UpdateRFI(rfiToBeSaved))
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

        // GET: Inventory/RFI/Create
        [SiteAuthorize(PermissionKey = "RFICreate")]
        public ActionResult Create(long? id)
        {
            bool loadCustomersAndOrders = CheckHasCustomerModule();
            var rfiresponse = rfiService.LoadRfiResponseData(id, loadCustomersAndOrders, "");
            RFIViewModel rfiViewModel = new RFIViewModel();
            if (rfiresponse.Rfi != null)
            {
                rfiViewModel.Rfi = rfiresponse.Rfi.CreateRfiServerToClient();
                rfiViewModel.Rfi.RequesterName = Resources.Shared.Common.TextDirection == "ltr" ? rfiresponse.RequesterNameE : rfiresponse.RequesterNameA;
                rfiViewModel.RfiItem = rfiresponse.RfiItem.Select(x => x.CreateRfiItemServerToClient()).ToList();
            }
            else
            {
                rfiViewModel.Rfi = new RFI
                {
                    RequesterName = Session["UserFullName"].ToString()
                };
                rfiViewModel.RfiItem = new List<RFIItem>();
            }
            if (loadCustomersAndOrders)
            {
                rfiViewModel.Customers = rfiresponse.Customers.Select(x => x.CreateForDashboard());
                rfiViewModel.Orders = rfiresponse.Orders.Select(x => x.CreateForDashboard());
                //set customerId
                if (rfiViewModel.Rfi.OrderId > 0)
                    rfiViewModel.Rfi.CustomerId = rfiViewModel.Orders.FirstOrDefault(x => x.OrderId == rfiViewModel.Rfi.OrderId).CustomerId;
            }
            rfiViewModel.ItemVariationDropDownList = rfiresponse.ItemVariationDropDownList;
            return View(rfiViewModel);
        }

        // POST: Inventory/RFI/Create
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(RFIViewModel rfiViewModel)
        {
            try
            {
                if (rfiViewModel.Rfi.RFIId > 0)
                {
                    rfiViewModel.Rfi.RecUpdatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecUpdatedDate = DateTime.Now;

                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.RFI.RFI.RFIUpdated,
                        IsUpdated = true
                    };
                }
                else
                {
                    rfiViewModel.Rfi.RecCreatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecCreatedDate = DateTime.Now;

                    rfiViewModel.Rfi.RecUpdatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecUpdatedDate = DateTime.Now;
                    TempData["message"] = new MessageViewModel { Message = Resources.RFI.RFI.RFICreated, IsSaved = true };
                }

                var rfiToBeSaved = rfiViewModel.CreateRfiClientToServer();
                if (rfiService.SaveRFI(rfiToBeSaved))
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

        [SiteAuthorize(PermissionKey = "RFIHistory")]
        public ActionResult History(long? id)
        {
            RfiHistoryResponse response = rfiService.GetRfiHistoryData(id);
            RFIHistoryViewModel viewModel = new RFIHistoryViewModel();
            viewModel.Rfis = response.Rfis.Any() ? response.Rfis.Select(x => x.CreateRfiServerToClient()).ToList() : new List<RFI>();
            viewModel.RfiItems = response.RfiItems != null ? response.RfiItems.Select(x => x.CreateRfiItemDetailsServerToClient()).ToList() : new List<RFIItem>();
            viewModel.RecentRfi = (response.RecentRfi != null && response.RecentRfi.RFIId > 0) ? response.RecentRfi.CreateRfiServerToClient() : new RFI();
            
            if (viewModel.RecentRfi != null)
            {
                viewModel.RecentRfi.RequesterName = response.RequesterNameEn;
                viewModel.RecentRfi.RequesterNameAr = response.RequesterNameAr;
                viewModel.RecentRfi.ManagerName = response.ManagerNameEn;
                viewModel.RecentRfi.ManagerNameAr = response.ManagerNameAr;
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult History(RFIHistoryViewModel viewModel)
        {
            try
            {
                viewModel.RecentRfi.RecUpdatedBy = User.Identity.GetUserId();
                viewModel.RecentRfi.RecUpdatedDate = DateTime.Now;
                viewModel.RecentRfi.ManagerId = User.Identity.GetUserId();

                TempData["message"] = new MessageViewModel
                {
                    Message = Resources.RFI.RFI.RFIReplied,
                    IsUpdated = true
                };

                var rfiToBeSaved = viewModel.RecentRfi.CreateRfiDetailsClientToServer();
                if (rfiService.UpdateRFI(rfiToBeSaved))
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