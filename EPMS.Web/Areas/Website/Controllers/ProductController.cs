using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ModelMappers.Website.ProductSection;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ViewModels.Product;
using EPMS.WebModels.WebsiteModels;
using EPMS.WebModels.WebsiteModels.Common;
using EPMS.Web.Controllers;
using EPMS.WebModels.ViewModels.Common;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class ProductController : BaseController
    {
        #region Private

        private readonly IProductService productService;
        private readonly IProductSectionService productSectionService;
        private readonly IInventoryDepartmentService departmentService;
        private readonly IItemVariationService itemVariationService;

        #endregion

        #region Constructor

        public ProductController(IProductService productService, IProductSectionService productSectionService, IInventoryDepartmentService departmentService, IItemVariationService itemVariationService)
        {
            this.productService = productService;
            this.productSectionService = productSectionService;
            this.departmentService = departmentService;
            this.itemVariationService = itemVariationService;
        }

        #endregion

        #region Public

        #region Index

        [SiteAuthorize(PermissionKey = "ProductIndex")]
        public ActionResult Index()
        {
            var searchRequest = new ProductSearchRequest();
            var viewModel = new ProductListViewModel
            {
                SearchRequest = searchRequest
            };
            ViewBag.IsIncludeNewJsTree = true;
            return View(viewModel);
            
        }

        [HttpPost]
        public ActionResult Index(ProductSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var products = productService.GetAllProducts(searchRequest);

            List<Product> productList = products.Products.Select(x => x.CreateFromServerToClient()).ToList();

            var viewModel = new ProductLvModel
            {
                aaData =  productList,
                iTotalRecords = products.TotalCount,
                iTotalDisplayRecords = products.TotalCount,
                sEcho = searchRequest.sEcho,
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Create

        [SiteAuthorize(PermissionKey = "ProductCreate")]
        public ActionResult Create(long? id)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            ProductResponse productResponse;
            if (id != null)
            {
                productResponse = productService.ProductResponse((long)id);
                productViewModel.Product = productResponse.Product.CreateFromServerToClient();
            }
            else
            {
                productResponse = productService.ProductResponse(0);
            }
            productViewModel.ProductSections =
                    productResponse.ProductSections.Where(x=>x.InventoyDepartmentId == null).Select(x => x.CreateFromServerToClient()).ToList();
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            if (productViewModel.Product.ProductId > 0)
            {
                ProductRequest productToSave = productViewModel.Product.CreateFromClientToServer();
                var response = productService.SaveProduct(productToSave);
                if(response.Status)
                {
                    TempData["message"] = new MessageViewModel { Message = WebModels.Resources.Website.Product.ProductCreate.UpdateMessage, IsUpdated = true };
                    return RedirectToAction("Index");
                }
                TempData["message"] = new MessageViewModel { Message = WebModels.Resources.Website.Product.ProductCreate.UpdateErrorMessage, IsUpdated = true };
                return View(productViewModel);
            }
            else
            {
                ProductRequest productToSave = productViewModel.Product.CreateFromClientToServer();
                var response = productService.SaveProduct(productToSave);
                if(response.Status)
                {
                    TempData["message"] = new MessageViewModel { Message = WebModels.Resources.Website.Product.ProductCreate.AddMessage, IsSaved = true };
                    return RedirectToAction("Index");
                }
                TempData["message"] = new MessageViewModel { Message = WebModels.Resources.Website.Product.ProductCreate.AddErrorMessage, IsUpdated = true };
                return View(productViewModel);
            }
        }

        #endregion

        #region Delete

        [HttpPost]
        public JsonResult DeleteIt(long productId)
        {
            string status = productService.DeleteProduct(productId);
            switch (status)
            {
                case "Success":
                    return Json(new { response = "OK" }, JsonRequestBehavior.AllowGet);
                case "Associated":
                    return Json(new { response = "ASSOCIATED" }, JsonRequestBehavior.AllowGet);
                case "Error":
                    return Json(new { response = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { response = "ERROR" }, JsonRequestBehavior.AllowGet);
        }

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

        #region Import

        [HttpPost]
        public JsonResult ImportProducts(string[] itemVariationIds, string[] sectionIds)
        {
            bool isProductAdded = false;
            // select products that are not in DB
            IList<long> noItemDuplication = productService.RemoveDuplication(itemVariationIds);
            // save Products
            IList<EPMS.Models.DomainModels.Product> productsToAdd = new List<EPMS.Models.DomainModels.Product>();
            foreach (var id in noItemDuplication)
            {
                EPMS.Models.DomainModels.Product addToList = new EPMS.Models.DomainModels.Product
                {
                    ItemVariationId = id,
                    RecCreatedBy = User.Identity.GetUserId(),
                    RecCreatedDt = DateTime.Now,
                    RecLastUpdatedBy = User.Identity.GetUserId(),
                    RecLastUpdatedDt = DateTime.Now
                };
                productsToAdd.Add(addToList);
            }
            if (productsToAdd.Any())
            {
                if (productService.SaveProducts(productsToAdd))
                {
                    isProductAdded = true;
                }
            }
            //// select product section that are not in DB
            //IList<long> noSectionDuplication = productSectionService.RemoveDuplication(sectionIds);
            //// save Product Sections
            //IList<EPMS.Models.DomainModels.ProductSection> productSectionsToAdd = new List<EPMS.Models.DomainModels.ProductSection>();
            //foreach (var id in noSectionDuplication)
            //{
            //    EPMS.Models.DomainModels.ProductSection addToList = new EPMS.Models.DomainModels.ProductSection
            //    {
            //        InventoyDepartmentId = id,
            //        ShowToPublic = true,
            //        RecCreatedBy = User.Identity.GetUserId(),
            //        RecCreatedDt = DateTime.Now,
            //        RecLastUpdatedBy = User.Identity.GetUserId(),
            //        RecLastUpdatedDt = DateTime.Now
            //    };
            //    productSectionsToAdd.Add(addToList);
            //}
            //if (productSectionsToAdd.Any())
            //{
            //    if (productSectionService.SaveProductSections(productSectionsToAdd))
            //    {
            //        isProductSectionAdded = true;
            //    }
            //}
            if ((isProductAdded || productsToAdd.Count == 0))
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Get Tree Data
        [HttpGet]
        public JsonResult GetTreeData(long? id, string direction)
        {
            var departments = departmentService.GetAll();
            IList<JsTreeJson> details = new List<JsTreeJson>();
            //JsTreeJson parent = new JsTreeJson
            //{
            //    id = "parentNode",
            //    text = Resources.Website.Product.ProductIndex.Departments,
            //    parent = "#"
            //};
            //details.Add(parent);
            foreach (var inventoryDepartment in departments)
            {
                details.Add(direction == "ltr"
                    ? inventoryDepartment.CreateForJsTreeJsonEn()
                    : inventoryDepartment.CreateForJsTreeJsonAr());
                if (inventoryDepartment.InventoryItems.Any())
                {
                    foreach (var inventoryItem in inventoryDepartment.InventoryItems)
                    {
                        if (inventoryItem.ItemVariations.Any())
                        {
                            foreach (var itemVariation in inventoryItem.ItemVariations)
                            {
                                JsTreeJson item = new JsTreeJson
                                {
                                    id = itemVariation.ItemVariationId + "_Item",
                                    text = direction == "ltr" ? itemVariation.SKUDescriptionEn : itemVariation.SKUDescriptionAr,
                                    parent = inventoryDepartment.DepartmentId + "_department"
                                };
                                details.Add(item);
                            }
                        }
                    }
                }
            }
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(details);
            return Json(serializedResult, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

    }
}