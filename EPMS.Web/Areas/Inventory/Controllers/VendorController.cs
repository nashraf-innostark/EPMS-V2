using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Vendor;
using EPMS.Models.RequestModels;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    //[SiteAuthorize(PermissionKey = "InvSys", IsModule = true)]
    public class VendorController : BaseController
    {

        #region Private

        private readonly IVendorService vendorService;
        private readonly IVendorItemsService vendorItemsService;

        #endregion

        #region Constructor

        public VendorController(IVendorService vendorService, IVendorItemsService vendorItemsService)
        {
            this.vendorService = vendorService;
            this.vendorItemsService = vendorItemsService;
        }

        #endregion

        #region Public

        #region Index
        public ActionResult Index()
        {
            return View(new VendorViewModel
            {
                VendorList = vendorService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }
        #endregion

        #region Create

        public ActionResult Create(long? id)
        {
            VendorViewModel vendorViewModel = new VendorViewModel();
            if (id != null)
            {
                vendorViewModel.Vendor = vendorService.FindVendorById((long)id).CreateFromServerToClient();
                vendorViewModel.Vendor.VendorItems = vendorItemsService.GetItemsByVendorId((long)id).ToList();
            }
            return View(vendorViewModel);
        }
        [HttpPost]
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