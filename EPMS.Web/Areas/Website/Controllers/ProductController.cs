using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Website.Product;
using EPMS.Web.ViewModels.Product;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class ProductController : BaseController
    {
        #region Private

        private readonly IProductService productService;

        #endregion

        #region Constructor

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        #endregion

        #region Public

        #region Index
        public ActionResult Index()
        {
            return View(new ProductListViewModel
            {
                Products = productService.GetAll().Select(x => x.CreateFromServerToClient())
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