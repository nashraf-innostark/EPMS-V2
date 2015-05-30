using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPMS.Implementation.Identity;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.ModelMappers;
using System.Configuration;
using System.Globalization;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Inventory.RFI;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Request;
using EPMS.Web.ViewModels.RFI;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RFI = EPMS.Web.Models.RFI;
using RFIItem = EPMS.Web.Models.RFIItem;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    //[SiteAuthorize(PermissionKey = "PMS", IsModule = true)]
    public class RFIController : BaseController
    {
        private readonly ICustomerService customerService;
        private readonly IOrdersService ordersService;
        private readonly IRFIService rfiService;

        public RFIController(ICustomerService customerService,IOrdersService ordersService,IRFIService rfiService)
        {
            this.customerService = customerService;
            this.ordersService = ordersService;
            this.rfiService = rfiService;
        }

        // GET: Inventory/RFI
        //[SiteAuthorize(PermissionKey = "RFIIndex")]
        public ActionResult Index()
        {
            RfiSearchRequest searchRequest = Session["PageMetaData"] as RfiSearchRequest;
            ViewBag.UserRole = Session["RoleName"].ToString();
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
            ViewBag.UserRole = Session["RoleName"].ToString();
            if (Session["RoleName"] != null && Session["RoleName"].ToString() == "Admin")
            {
                searchRequest.Requester = "Admin";
            }
            else
            {
                searchRequest.Requester = Session["UserID"].ToString();
            }
            var requestResponse = rfiService.LoadAllRfis(searchRequest);
            var data = requestResponse.Rfis.Select(x => x.CreateRfiServerToClient());
            var employeeRequests = data as IList<RFI> ?? data.ToList();
            if (employeeRequests.Any())
            {
                viewModel.aaData = employeeRequests;
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
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Details(RFIViewModel rfiViewModel)
        {
            return View();
        }

        // GET: Inventory/RFI/Create
        public ActionResult Create(long? id)
        {
            bool loadCustomersAndOrders = CheckHasCustomerModule();
            var rfiresponse = rfiService.LoadRfiResponseData(id, loadCustomersAndOrders);
            RFIViewModel rfiViewModel = new RFIViewModel();
            if (rfiresponse.Rfi != null)
            {
                rfiViewModel.Rfi = rfiresponse.Rfi.CreateRfiServerToClient();
                rfiViewModel.Rfi.RecCreatedByName = rfiresponse.RecCreatedByName;
                rfiViewModel.RfiItem = rfiresponse.RfiItem.Select(x => x.CreateRfiItemServerToClient()).ToList();
            }
            else
            {
                rfiViewModel.Rfi = new RFI
                {
                    RecCreatedByName = Session["FullName"].ToString()
                };
                rfiViewModel.RfiItem = new List<RFIItem>();
            }
            if (loadCustomersAndOrders)
            {
                rfiViewModel.Customers = rfiresponse.Customers.Select(x => x.CreateForDashboard());
                rfiViewModel.Orders = rfiresponse.Orders.Select(x => x.CreateForDashboard());
                //set customerId
                if (rfiViewModel.Rfi.OrderId>0)
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
                        Message = Resources.HR.Request.RequestReplied,
                        IsUpdated = true
                    };
                }
                else
                {
                    rfiViewModel.Rfi.RecCreatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecCreatedDate = DateTime.Now;

                    rfiViewModel.Rfi.RecUpdatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecUpdatedDate = DateTime.Now;
                    TempData["message"] = new MessageViewModel { Message = Resources.HR.Request.RequestCreated, IsSaved = true };
                }
                
                var rfiToBeSaved = rfiViewModel.CreateRfiClientToServer();
                if(rfiService.SaveRFI(rfiToBeSaved))
                {
                    //success
                }
                //failed to save
                return View(); 
            }
            catch
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
        // GET: Inventory/RFI/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inventory/RFI/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/RFI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventory/RFI/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
