using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
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

        #region Upload Image

        public ActionResult UploadImage()
        {
            HttpPostedFileBase image = Request.Files[0];
            var filename = "";
            try
            {
                //Save File to Folder
                if ((image != null))
                {
                    filename =
                        (DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(".", "") + image.FileName)
                            .Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["ProductImage"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    image.SaveAs(savedFileName);
                }
            }
            catch (Exception exp)
            {
                return
                    Json(
                        new
                        {
                            response = "Failed to upload. Error: " + exp.Message,
                            status = (int)HttpStatusCode.BadRequest
                        }, JsonRequestBehavior.AllowGet);
            }
            return
                Json(
                    new
                    {
                        filename = filename,
                        size = image.ContentLength / 1024 + "KB",
                        response = "Successfully uploaded!",
                        status = (int)HttpStatusCode.OK
                    }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

    }
}