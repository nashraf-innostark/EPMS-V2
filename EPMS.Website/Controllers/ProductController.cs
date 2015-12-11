using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Product;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.Website.Controllers
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
            viewModel.ProductSections = productsList.ProductSections.ToList();
            if (productsList.Products.Any())
            {
                viewModel.Products = productsList.Products.Select(x => x.CreateFromServerToClientFromInventory()).ToList();
                viewModel.SearchRequest.TotalCount = productsList.TotalCount;
            }
            else
            {
                viewModel.Products = new List<Product>();
                viewModel.SearchRequest.TotalCount = productsList.TotalCount;
            }
            // New Arrivals
            var newArrivals = productsList.AllProducts.Where(x => x.NewArrival).OrderByDescending(x => x.RecCreatedDt).Select(x => x.CreateFromServerToClientFromInventory()).GroupBy(x=>x.InventoryItemId);
            foreach (var newArrival in newArrivals)
            {
                viewModel.NewArrivals.Add(newArrival.FirstOrDefault());
            }
            // Best Sell
            var bestSells = productsList.AllProducts.Where(x => x.BestSeller).OrderByDescending(x => x.RecCreatedDt).Select(x => x.CreateFromServerToClientFromInventory()).GroupBy(x => x.InventoryItemId);
            foreach (var bestSell in bestSells)
            {
                viewModel.BestSell.Add(bestSell.FirstOrDefault());
            }
            // Random Products
            var randomProducts = productsList.AllProducts.Where(x => x.RandomProduct).OrderByDescending(x => x.RecCreatedDt).Select(x => x.CreateFromServerToClientFromInventory()).GroupBy(x => x.InventoryItemId);
            foreach (var randomProduct in randomProducts)
            {
                viewModel.RandomProducts.Add(randomProduct.FirstOrDefault());
            }
            // Featured Products
            var featuredProducts = productsList.AllProducts.Where(x => x.Featured).OrderByDescending(x => x.RecCreatedDt).Select(x => x.CreateFromServerToClientFromInventory()).GroupBy(x => x.InventoryItemId);
            foreach (var featuredProduct in featuredProducts)
            {
                viewModel.FeaturedProducts.Add(featuredProduct.FirstOrDefault());
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            ViewBag.From = from;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ProductListViewModel viewModel)
        {
            ProductsListResponse productsList = productService.GetProductsList(viewModel.SearchRequest);
            viewModel.ProductSections = productsList.ProductSections.ToList();
            if (productsList.Products.Any())
            {
                viewModel.Products = productsList.Products.Select(x => x.CreateFromServerToClientFromInventory()).ToList();
                viewModel.SearchRequest.TotalCount = productsList.TotalCount;
            }
            else
            {
                viewModel.Products = new List<Product>();
                viewModel.SearchRequest.TotalCount = productsList.TotalCount;
            }
            // New Arrivals
            var newArrivals = productsList.AllProducts.Where(x => x.NewArrival).OrderByDescending(x => x.RecCreatedDt).Select(x => x.CreateFromServerToClientFromInventory()).GroupBy(x => x.InventoryItemId);
            foreach (var newArrival in newArrivals)
            {
                viewModel.NewArrivals.Add(newArrival.FirstOrDefault());
            }
            // Best Sell
            var bestSells = productsList.AllProducts.Where(x => x.BestSeller).OrderByDescending(x => x.RecCreatedDt).Select(x => x.CreateFromServerToClientFromInventory()).GroupBy(x => x.InventoryItemId);
            foreach (var bestSell in bestSells)
            {
                viewModel.BestSell.Add(bestSell.FirstOrDefault());
            }
            // Random Products
            var randomProducts = productsList.AllProducts.Where(x => x.RandomProduct).OrderByDescending(x => x.RecCreatedDt).Select(x => x.CreateFromServerToClientFromInventory()).GroupBy(x => x.InventoryItemId);
            foreach (var randomProduct in randomProducts)
            {
                viewModel.RandomProducts.Add(randomProduct.FirstOrDefault());
            }
            // Featured Products
            var featuredProducts = productsList.AllProducts.Where(x => x.Featured).OrderByDescending(x => x.RecCreatedDt).Select(x => x.CreateFromServerToClientFromInventory()).GroupBy(x => x.InventoryItemId);
            foreach (var featuredProduct in featuredProducts)
            {
                viewModel.FeaturedProducts.Add(featuredProduct.FirstOrDefault());
            }
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
            viewModel.ProductSections = productDetails.ProductSections;
            viewModel.Product = productDetails.Product.CreateFromServerToClientFromInventory();
            viewModel.ProductSizes = productDetails.ProductSizes;
            ViewBag.From = from;
            return View(viewModel);
        }

        #endregion

        #endregion

        //// GET: Product
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //public ActionResult Details(long id)
        //{
        //    return View();
        //}
    }
}