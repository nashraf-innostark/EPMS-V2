using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ViewModels.Product;
using EPMS.WebModels.ViewModels.Project;
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
        public ActionResult Index(long? id, string from)
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = new List<Product>()
            };
            long productId = 0;
            if (id != null)
            {
                productId = (long)id;
            }
            viewModel.SearchRequest = new ProductSearchRequest
            {
                Id = productId,
                From = from,
                SearchString = "",
                iDisplayLength = 3,
                SortBy = 1,
                SortDirection = "asc"
            };
            ProductsListResponse productsList = productService.GetProductsList(viewModel.SearchRequest);
            if (productsList.Products.Any())
            {
                viewModel.Products = productsList.Products.Select(x => x.CreateFromServerToClientFromInventory()).ToList();
                viewModel.SearchRequest.TotalCount = productsList.TotalCount;
            }
            ViewBag.ShowSlider = false;
            ViewBag.From = from;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ProductListViewModel viewModel)
        {
            ProductsListResponse productsList = productService.GetProductsList(viewModel.SearchRequest);
            if (productsList.Products.Any())
            {
                viewModel.Products = productsList.Products.Select(x => x.CreateFromServerToClientFromInventory()).ToList();
                viewModel.SearchRequest.TotalCount = productsList.TotalCount;
            }
            ViewBag.ShowSlider = false;
            ViewBag.From = viewModel.SearchRequest.From;
            return View(viewModel);
        }

        #endregion

        #region Details
        public ActionResult Details(long? id, string from)
        {
            ProductDetailViewModel viewModel = new ProductDetailViewModel
            {
                Product = new Product()
            };
            long productId = 0;
            if (id != null)
            {
                productId = (long)id;
            }
            ProductDetailResponse productDetails = productService.GetProductDetails(productId, from);
            switch (from)
            {
                case "Inventory":
                    if (productDetails.Product != null)
                    {
                        viewModel.Product = productDetails.ItemVariation.CreateFromItemVariation();
                    }
                    break;
                case "Sections":
                    if (productDetails.Product != null)
                    {
                        viewModel.Product = productDetails.Product.CreateFromServerToClientFromInventory();
                    }
                    break;
            }
            
            ViewBag.ShowSlider = false;
            ViewBag.From = from;
            return View(viewModel);
        }

        #endregion

        #endregion
    }
}