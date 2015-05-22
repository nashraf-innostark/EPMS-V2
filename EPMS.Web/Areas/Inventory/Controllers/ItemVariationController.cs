using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.ItemVariation;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class ItemVariationController : BaseController
    {
        #region Private

        private readonly IItemVariationService itemVariationService;

        #endregion

        #region Constructor

        public ItemVariationController(IItemVariationService itemVariationService)
        {
            this.itemVariationService = itemVariationService;
        }

        #endregion

        #region Public

        public ActionResult Create(long? id)
        {
            ItemVariationViewModel variationViewModel = new ItemVariationViewModel();
            if (id != null)
            {
                variationViewModel.ItemVariation = itemVariationService.FindVariationById((long)id).CreateFromServerToClient();
            }
            return View(variationViewModel);
        }

        #endregion
    }
}