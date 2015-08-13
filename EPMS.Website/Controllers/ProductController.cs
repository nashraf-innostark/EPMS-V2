using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ViewModels.Product;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.Website.Controllers
{
    public class ProductController : Controller
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
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Details
        public ActionResult Details(long? id, string from)
        {
            ProductDetailViewModel viewModel = new ProductDetailViewModel
            {
                Products = new List<Product>()
            };
            long productId = 0;
            if (id != null)
            {
                productId = (long) id;
            }
            ProductDetails responseDetails = productService.GetProductDetails(productId, from);
            if (responseDetails.Products.Any())
            {
                viewModel.Products = responseDetails.Products.Select(x=>x.CreateFromServerToClient()).ToList();
            }
            ViewBag.ShowSlider = false;
            return View(viewModel);
        }

        #endregion

        #endregion
    }
}