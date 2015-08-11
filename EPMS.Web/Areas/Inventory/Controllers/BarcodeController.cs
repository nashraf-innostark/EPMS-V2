﻿using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.Repository;
using EPMS.WebModels.ViewModels.Barcode;
using EPMS.Web.Controllers;

namespace EPMS.Web.Areas.Inventory.Controllers
{
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
        public ActionResult Index()
        {
            BarcodeViewModel viewModel = new BarcodeViewModel();
            viewModel.ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList().ToList();
            return View(viewModel);
        }
    }
}