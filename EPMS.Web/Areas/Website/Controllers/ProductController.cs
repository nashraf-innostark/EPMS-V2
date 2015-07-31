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
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Website.Product;
using EPMS.Web.ModelMappers.Website.ProductSection;
using EPMS.Web.Models.Common;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Product;
using Microsoft.AspNet.Identity;
using EPMS.Web.ModelMappers;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class ProductController : BaseController
    {
        #region Private

        private readonly IProductService productService;
        private readonly IProductSectionService productSectionService;
        private readonly IInventoryDepartmentService departmentService;

        #endregion

        #region Constructor

        public ProductController(IProductService productService, IProductSectionService productSectionService, IInventoryDepartmentService departmentService)
        {
            this.productService = productService;
            this.productSectionService = productSectionService;
            this.departmentService = departmentService;
        }

        #endregion

        #region Public

        #region Index
        public ActionResult Index()
        {
            ViewBag.IsIncludeNewJsTree = true;
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

        #region Import

        [HttpPost]
        public JsonResult ImportProducts(string[] itemVariationIds, string[] sectionIds)
        {
            bool isProductAdded = false;
            bool isProductSectionAdded = false;
            // save Products
            IList<EPMS.Models.DomainModels.Product> productsToAdd = new List<EPMS.Models.DomainModels.Product>();
            foreach (var product in itemVariationIds.ToList())
            {
                if (product.Contains("Item"))
                {
                    var id = Convert.ToInt64(product.Split('_')[0]);
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
            }
            if (productService.SaveProducts(productsToAdd))
            {
                isProductAdded = true;
            }
            // save Product Sections
            IList<EPMS.Models.DomainModels.ProductSection> productSectionsToAdd = new List<EPMS.Models.DomainModels.ProductSection>();
            foreach (var section in sectionIds.ToList())
            {
                if (section.Contains("department"))
                {
                    var id = Convert.ToInt64(section.Split('_')[0]);
                    EPMS.Models.DomainModels.ProductSection addToList = new EPMS.Models.DomainModels.ProductSection
                    {
                        InventoyDepartmentId = id,
                        ShowToPublic = true,
                        RecCreatedBy = User.Identity.GetUserId(),
                        RecCreatedDt = DateTime.Now,
                        RecLastUpdatedBy = User.Identity.GetUserId(),
                        RecLastUpdatedDt = DateTime.Now
                    };
                    productSectionsToAdd.Add(addToList);
                }
            }
            if (productSectionService.SaveProductSections(productSectionsToAdd))
            {
                isProductSectionAdded = true;
            }
            if (isProductAdded && isProductSectionAdded)
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
            foreach (var inventoryDepartment in departments)
            {
                if (direction == "ltr")
                {
                    details.Add(inventoryDepartment.CreateForJsTreeJsonEn());
                }
                else
                {
                    details.Add(inventoryDepartment.CreateForJsTreeJsonAr());
                }
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