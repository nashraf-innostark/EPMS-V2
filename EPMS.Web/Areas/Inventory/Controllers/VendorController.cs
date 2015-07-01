using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Vendor;
using EPMS.Models.RequestModels;
using EPMS.WebBase.Mvc;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "IS", IsModule = true)]
    public class VendorController : BaseController
    {

        #region Private

        private readonly IVendorService vendorService;
        private readonly IVendorItemsService vendorItemsService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IItemVariationService itemVariationService;

        #endregion

        #region Constructor

        public VendorController(IVendorService vendorService, IVendorItemsService vendorItemsService,
            IPurchaseOrderService purchaseOrderService, IItemVariationService itemVariationService)
        {
            this.vendorService = vendorService;
            this.vendorItemsService = vendorItemsService;
            this.purchaseOrderService = purchaseOrderService;
            this.itemVariationService = itemVariationService;
        }

        #endregion

        #region Public

        #region Index
        [SiteAuthorize(PermissionKey = "VendorIndex")]
        public ActionResult Index()
        {
            return View(new VendorViewModel
            {
                VendorList = vendorService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }
        #endregion

        #region Create
        [SiteAuthorize(PermissionKey = "VendorCreate")]
        public ActionResult Create(long? id)
        {
            VendorViewModel vendorViewModel = new VendorViewModel();
            if (id != null)
            {
                vendorViewModel.Vendor = vendorService.FindVendorById((long)id).CreateFromServerToClient();
                vendorViewModel.Vendor.VendorItems = vendorItemsService.GetItemsByVendorId((long)id).ToList();
                vendorViewModel.PurchaseOrders = purchaseOrderService.FindPoByVendorId(id).Select(x=>x.CreateFromServerToClient());
            }
            vendorViewModel.ItemVariationDropDownList = itemVariationService.GetItemVariationDropDownList();
            return View(vendorViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(VendorViewModel vendorViewModel)
        {
            VendorRequest vendorToSave = vendorViewModel.Vendor.CreateFromClientToServer();
            vendorService.SaveVendor(vendorToSave);
            {
                TempData["message"] = new MessageViewModel { Message = "Added", IsSaved = true };
                return RedirectToAction("Index");
            }
        }

        #endregion

        #endregion

    }
}