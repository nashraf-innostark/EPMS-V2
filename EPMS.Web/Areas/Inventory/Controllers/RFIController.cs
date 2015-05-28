using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Web.ModelMappers;
using System.Configuration;
using System.Globalization;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Inventory.RFI;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.RFI;
using Microsoft.AspNet.Identity;

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
        public ActionResult Index()
        {
            return View();
        }

        // GET: Inventory/RFI/Details/5
        public ActionResult Details(int id)
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
        public ActionResult Create(RFIViewModel rfiViewModel)
        {
            try
            {
                if (rfiViewModel.Rfi.RFIId > 0)
                {
                    rfiViewModel.Rfi.RecUpdatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecUpdatedDate = DateTime.Now;
                }
                else
                {
                    rfiViewModel.Rfi.RecCreatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecCreatedDate = DateTime.Now;

                    rfiViewModel.Rfi.RecUpdatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecUpdatedDate = DateTime.Now;
                }
                
                var rfiToBeSaved = rfiViewModel.CreateRfiClientToServer();
                if(rfiService.SaveRFI(rfiToBeSaved))
                {
                    //success
                }
                //failed to save
                return RedirectToAction("Index");
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
