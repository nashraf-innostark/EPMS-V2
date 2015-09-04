using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ViewModels.Product;

namespace EPMS.Website.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly IProductService productService;

        public CatalogueController(IProductService productService)
        {
            this.productService = productService;
        }

        public ActionResult Index()
        {
            ViewBag.ShowSlider = false;

            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = productService.GetAll().Select(x => x.CreateFromServerToClient()).ToList()
            };
            return View(viewModel);
        }
    }
}