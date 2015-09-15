using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Product;

namespace EPMS.Website.Controllers
{
    public class CatalogueController : BaseController
    {
        private readonly IProductService productService;

        public CatalogueController(IProductService productService)
        {
            this.productService = productService;
        }

        public ActionResult Index()
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = productService.GetAll().Select(x => x.CreateFromServerToClient()).ToList()
            };
            foreach (var product in viewModel.Products)
            {
                
                
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        public ActionResult Test()
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = productService.GetAll().Select(x => x.CreateFromServerToClient()).ToList()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
    }
}