using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Website.Product;
using EPMS.Web.ModelMappers.Website.ProductSection;
using EPMS.Web.ViewModels.Common;
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

        public ActionResult Create(long? id)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            ProductResponse productResponse;
            if (id != null)
            {
                productResponse = productService.ProductResponse((long) id);
                productViewModel.Product = productResponse.Product.CreateFromServerToClient();
            }
            else
            {
                productResponse = productService.ProductResponse(0);
            }
            productViewModel.ProductSections =
                    productResponse.ProductSections.Select(x => x.CreateFromServerToClient()).ToList();
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            if (productViewModel.Product.ProductId > 0)
            {
                ProductRequest productToSave = productViewModel.Product.CreateFromClientToServer();
                productService.SaveProduct(productToSave);
                {
                    TempData["message"] = new MessageViewModel {Message = "Updated", IsUpdated = true};
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ProductRequest productToSave = productViewModel.Product.CreateFromClientToServer();
                productService.SaveProduct(productToSave);
                {
                    TempData["message"] = new MessageViewModel {Message = "Saved", IsSaved = true};
                    return RedirectToAction("Index");
                }
            }
        }

        #endregion

        #region Delete
        #endregion

        #endregion

    }
}