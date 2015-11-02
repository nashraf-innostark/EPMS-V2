﻿using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.Repository;
using EPMS.WebModels.ViewModels.Barcode;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "IS", IsModule = true)]
    public class BarcodeController : BaseController
    {
        #region Private

        private readonly IItemVariationRepository itemVariationRepository;
        
        #endregion

        #region Constructor
        public BarcodeController(IItemVariationRepository itemVariationRepository)
        {
            this.itemVariationRepository = itemVariationRepository;
        }

        #endregion

        // GET: Inventory/Barcode
        [SiteAuthorize(PermissionKey = "BarcodeIndex")]
        public ActionResult Index()
        {
            BarcodeViewModel viewModel = new BarcodeViewModel();
            viewModel.ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList().ToList();
            ViewBag.IsIncludeNewJsTree = true;
            return View(viewModel);
        }
    }
}