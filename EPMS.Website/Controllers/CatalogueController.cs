using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.Resources.Shared;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Product;
using Rotativa;

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
            ViewBag.ProductsCount = productService.GetProductsCount();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(new ProductListViewModel());
        }

        // Generate PDF
        public ActionResult Catalogue()
        {
            return new ActionAsPdf("SteveJobsHtml") { FileName = "EPMS Catalogue.pdf" };
            //return new ActionAsPdf("Table") { FileName = "Table.pdf" };
        }

        // view used to generate PDF
        public ActionResult SteveJobsHtml()
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = productService.GetAllSortedProducts().Select(x => x.CreateForCatalogue()).ToList()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        public ActionResult Table()
        {
            return View();
        }

        // load catalogue page
        [HttpGet]
        public JsonResult LoadPage(int pageNo)
        {
            var product = productService.GetProductForCatalog(pageNo).CreateForCatalogue();
            var direction = Common.TextDirection;
            string cpLink = ConfigurationManager.AppSettings["CpLink"];
            var imageSrc = "";
            if (product.ItemVariationId != null)
            {
                if (string.IsNullOrEmpty(product.ItemImage))
                {
                    imageSrc = cpLink + "/Images/ProductImage/noimage_department.png";
                }
                else
                {
                    imageSrc = cpLink + "/Images/ItemImage/" + product.ItemImage;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(product.ProductImage))
                {
                    imageSrc = cpLink + "/Images/ProductImage/noimage_department.png";
                }
                else
                {
                    imageSrc = cpLink + "/Images/ProductImage/" + product.ProductImage;
                }
            }
            string html = product.ItemVariationId != null ? GetPageForInventoryItem(product, imageSrc, pageNo, direction) : 
                GetPageForProductItem(product, imageSrc, pageNo, direction);
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public string GetPageForInventoryItem(WebModels.WebsiteModels.Product product, string imageSrc, int pageNo, string direction)
        {
            string header;
            if (direction == "ltr")
            {
                header = "<h6 class=\"titleLeft\">" + (direction == "ltr" ? product.DepartmentNameEn : product.DepartmentNameAr) + "</h6><h6 class=\"titleRight\">" + product.PathTillParent + "</h6>";
            }
            else
            {
                header = "<h6 class=\"titleLeft\">" + product.PathTillParent + "</h6><h6 class=\"titleRight\">" + (direction == "ltr" ? product.DepartmentNameEn : product.DepartmentNameAr) + "</h6>";
            }
            string footer;
            if (string.IsNullOrEmpty(product.DeptColor))
                product.DeptColor = "rgb(8, 92, 101)";
            if (!string.IsNullOrEmpty(product.DeptColor))
            {
                footer = "<div class=\"catalogue-footer\" style=\"background-color:" + product.DeptColor + "; color: white;\">" +
                            "<h5 style=\"color: white; text-align: right; margin-right: 10px;\">" + pageNo + "</h5>" +
                         "</div>";
            }
            else
            {
                footer = "<div class=\"catalogue-footer\" style=\"background-color: #000000; \">" +
                            "<h5 style=\"color: #ffffff; text-align: right; margin-right:10px;\">" + pageNo + "</h5>" +
                         "</div>";
            }
            string dir = "";
            if (direction == "rtl")
            {
                dir = "direction: rtl";
            }
            string html = "<div class=\"book-content catalog\" style=\""+dir+"\">" +
                            "<div class=\"catalogDepartment\" style=\"background-color: " + product.DeptColor + "; color: white;\">" +
                                header +
                            "</div>" +
                            "<div style=\"color:" + product.DeptColor + ";\">" +
                                "<h6>" + (direction == "ltr" ? product.ItemNameEn : product.ItemNameAr) + "</h6>" +
                            "</div>" +
                            "<br /><br />" +
                            "<img style=\"width: 125; height: 125px;\" src=\"" + imageSrc + "\" alt=\"user\">" +
                            "<h6 style=\"color:" + product.DeptColor + ";\">" + WebModels.Resources.WebsiteClient.Catalogue.Catalogue.ProductDescription + "</h6>" +
                            "<p>" + product.ItemDesc + "</p>" +
                            "<h6 style=\"color:" + product.DeptColor + ";\">" + WebModels.Resources.WebsiteClient.Catalogue.Catalogue.ProductSpecification + "</h6>" +
                            "<p>" + (direction == "ltr" ? product.ProductSpecificationEn : product.ProductSpecificationAr) + "</p>" +
                            footer +
                        "</div>";
            return html;
        }

        public string GetPageForProductItem(WebModels.WebsiteModels.Product product, string imageSrc, int pageNo, string direction)
        {
            string footer;
            if (string.IsNullOrEmpty(product.DeptColor))
                product.DeptColor = "rgb(8, 92, 101)";
            if (!string.IsNullOrEmpty(product.DeptColor))
            {
                footer = "<div class=\"catalogue-footer\" style=\"background-color:" + product.DeptColor + "; color: white;\">" +
                            "<h5 style=\"color: white; text-align: right; margin-right: 10px;\">" + pageNo + "</h5>" +
                         "</div>";
            }
            else
            {
                footer = "<div class=\"catalogue-footer\" style=\"background-color: #000000; \">" +
                            "<h5 style=\"color: #ffffff; text-align: right; margin-right:10px;\">" + pageNo + "</h5>" +
                         "</div>";
            }
            string html = "<div class=\"book-content catalog\">" +
                            "<div class=\"catalogDepartment\" style=\"background-color: rgb(8, 92, 101); color: white;\">" +
                                "<h6 class=\"titleLeft\">" + (direction == "ltr" ? product.DepartmentNameEn : product.DepartmentNameAr) + "</h6><h6 class=\"titleRight\">" + product.PathTillParent + "</h6>" +
                            "</div>" +
                            "<div style=\"color: " + product.DeptColor + "\">" +
                                "<h6><span style=\"float: left\">" + (direction == "ltr" ? product.ProductNameEn : product.ProductNameAr) + "</span></h6>" +
                            "</div>" +
                            "<br /><br />" +
                            "<img style=\"width: 125px; height: 125px;\" src=\"" + imageSrc + "\" alt=\"user\">" +
                            "<h6 style=\"color: " + product.DeptColor + "\">" + WebModels.Resources.WebsiteClient.Catalogue.Catalogue.ProductDescription + "</h6>" +
                            "<p>" + (direction == "ltr" ? product.ProductDescEn : product.ProductDescAr) + "</p>" +
                            "<h6 style=\"color: " + product.DeptColor + ";\">" + WebModels.Resources.WebsiteClient.Catalogue.Catalogue.ProductSpecification + "</h6>" +
                            "<p>" + (direction == "ltr" ? product.ProductSpecificationEn : product.ProductSpecificationAr) + "</p>" +
                            footer +
                        "</div>";
            return html;
        }
    }
}