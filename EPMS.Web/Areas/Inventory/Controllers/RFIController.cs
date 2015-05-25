using System.Linq;
using EPMS.Web.ModelMappers;
using System.Configuration;
using System.Globalization;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ViewModels.RFI;

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
        public ActionResult Create()
        {
            RFIViewModel rfiViewModel =new RFIViewModel();
            rfiViewModel.Rfi.RecCreatedByName = Session["FullName"].ToString();

            CheckHasCustomerModule(rfiViewModel);
            return View(rfiViewModel);
        }

        // POST: Inventory/RFI/Create
        [HttpPost]
        public ActionResult Create(RFIViewModel rfiViewModel)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private void CheckHasCustomerModule(RFIViewModel viewModel)
        {
            // check license
            var licenseKeyEncrypted = ConfigurationManager.AppSettings["LicenseKey"].ToString(CultureInfo.InvariantCulture);
            string LicenseKey = WebBase.EncryptDecrypt.StringCipher.Decrypt(licenseKeyEncrypted, "123");
            var splitLicenseKey = LicenseKey.Split('|');
            string[] Modules = splitLicenseKey[4].Split(';');
            if (Modules.Contains("CS") || Modules.Contains("Customer Service"))
            {
                var customers = customerService.GetAll();
                var orders = ordersService.GetAll();
                viewModel.Customers = customers.Select(x => x.CreateForDashboard());
                viewModel.Orders = orders.Select(x => x.CreateForDashboard());
               
                ViewBag.HasModule = true;
            }
            else
            {
                ViewBag.HasModule = false;
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
