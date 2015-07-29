using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Website.ProductSection;
using EPMS.Web.ViewModels.ProductSection;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class ProductSectionController : BaseController
    {
        #region Private

        private readonly IProductSectionService productSectionService;

        #endregion

        #region Constructor

        public ProductSectionController(IProductSectionService productSectionService)
        {
            this.productSectionService = productSectionService;
        }

        #endregion

        #region Public

        #region Index
        public ActionResult Index()
        {
            return View(new ProductSectionListViewModel
            {
                ProductSections = productSectionService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        #endregion

        #region Create
        #endregion

        #region Delete
        #endregion

        #endregion

    }
}