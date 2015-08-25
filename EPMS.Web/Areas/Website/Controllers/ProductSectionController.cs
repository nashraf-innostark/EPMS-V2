using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.ProductSection;
using EPMS.WebModels.ViewModels.ProductSection;
using EPMS.WebModels.WebsiteModels;
using EPMS.WebModels.WebsiteModels.Common;
using EPMS.Web.Controllers;
using EPMS.WebModels.ViewModels.Common;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class ProductSectionController : BaseController
    {
        #region Private

        private readonly IProductSectionService productSectionService;

        #endregion

        #region Constructor

        public ProductSectionController(IProductSectionService productSectionService)
        {
            this.productSectionService = productSectionService;
        }

        #endregion

        #region Public

        #region Index
        public ActionResult Index()
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(new ProductSectionListViewModel
            {
                ProductSections = productSectionService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        #endregion

        #region Create

        public ActionResult Create(long? id)
        {
            var productSections = productSectionService.GetAll().Select(x => x.CreateFromServerToClientForTree());
            var productSectionCreate = ProductSectionCreate(productSections);
            ProductSectionViewModel viewModel = new ProductSectionViewModel
            {
                ProductSections = productSectionCreate.ProductSections,
                ProductSectionsChildList = productSectionCreate.ProductSectionsChildList
            };
            // Javascript Serializer
            var serializer = new JavaScriptSerializer();
            ViewBag.JsTree = serializer.Serialize(productSectionCreate.JsTreeJsons);
            // use new version of JsTree
            ViewBag.IsIncludeNewJsTree = true;
            ViewBag.ProductId = id != null ? (long)id : 0;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductSectionViewModel viewModel)
        {
            try
            {
                if (viewModel.ProductSection.SectionId > 0)
                {
                    viewModel.ProductSection.RecLastUpdatedBy = User.Identity.GetUserId();
                    viewModel.ProductSection.RecLastUpdatedDt = DateTime.Now;
                    EPMS.Models.DomainModels.ProductSection productSectionToUpdate = viewModel.ProductSection.CreateFromClientToServer();
                    if (productSectionService.UpdateProductSection(productSectionToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Product Section Successfully Updated",
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    viewModel.ProductSection.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.ProductSection.RecCreatedDt = DateTime.Now;
                    viewModel.ProductSection.RecLastUpdatedBy = User.Identity.GetUserId();
                    viewModel.ProductSection.RecLastUpdatedDt = DateTime.Now;
                    EPMS.Models.DomainModels.ProductSection productSectionToAdd = viewModel.ProductSection.CreateFromClientToServer();
                    if (productSectionService.AddProductSection(productSectionToAdd))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Product Section Successfully Added",
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                return View(viewModel);
            }
            return View(viewModel);
        }

        #endregion

        #region Delete
        #endregion

        #region JsTree

        private ProductSectionCreateViewModel ProductSectionCreate(IEnumerable<ProductSection> productSections)
        {
            ProductSectionCreateViewModel model = new ProductSectionCreateViewModel
            {
                JsTreeJsons = new List<JsTreeJson>(),
                ProductSections = new List<ProductSection>(),
                ProductSectionsChildList = new List<ProductSectionsListForTree>()
            };
            JsTreeJson parent = new JsTreeJson
            {
                id = "parentNode",
                text = EPMS.WebModels.Resources.Website.Product.ProductIndex.Departments,
                parent = "#"
            };
            model.JsTreeJsons.Add(parent);
            string direction = EPMS.WebModels.Resources.Shared.Common.TextDirection;
            foreach (var productSection in productSections)
            {
                // add root node
                JsTreeJson jsTree;
                if (productSection.InventoyDepartmentId != null)
                {
                    jsTree = new JsTreeJson
                    {
                        id = productSection.SectionId + "_Imported",
                        parent = productSection.ParentSectionId != null ? productSection.ParentSectionId.ToString() : "parentNode",
                        text = direction == "ltr" ? productSection.InventoryDepartmentNameEn : productSection.InventoryDepartmentNameAr
                    };
                }
                else
                {
                    jsTree = new JsTreeJson
                    {
                        id = productSection.SectionId.ToString(),
                        parent = productSection.ParentSectionId != null ? productSection.ParentSectionId.ToString() : "parentNode",
                        text = direction == "ltr" ? productSection.SectionNameEn : productSection.SectionNameAr
                    };
                }
                model.JsTreeJsons.Add(jsTree);
                model.ProductSections.Add(productSection);
                // check if child department exist
                if (productSection.InventoryDepartment != null)
                {
                    // check if have 1st level child
                    if (productSection.InventoryDepartment.InventoryDepartments != null &&
                        productSection.InventoryDepartment.InventoryDepartments.Any())
                    {
                        var subDepartments = productSection.InventoryDepartment.InventoryDepartments;
                        // add all 1st level childs 
                        foreach (var subDepartment in subDepartments)
                        {
                            JsTreeJson jsTreeSub = new JsTreeJson
                            {
                                id = subDepartment.DepartmentId + "_ImportedSub",
                                parent = jsTree.id,
                                text = direction == "ltr" ? subDepartment.DepartmentNameEn : subDepartment.DepartmentNameAr
                            };
                            model.JsTreeJsons.Add(jsTreeSub);
                            ProductSectionsListForTree subSectionToAdd = new ProductSectionsListForTree
                            {
                                id = subDepartment.DepartmentId + "_ImportedSub",
                                parent = jsTree.id,
                                textEn = subDepartment.DepartmentNameEn,
                                textAr = subDepartment.DepartmentNameAr,
                                DepartmentId = subDepartment.DepartmentId
                            };
                            model.ProductSectionsChildList.Add(subSectionToAdd);
                            // check if 2nd level child
                            if (subDepartment.InventoryDepartments != null && subDepartment.InventoryDepartments.Any())
                            {
                                var subSubDepartments = subDepartment.InventoryDepartments;
                                // add all second level childs
                                foreach (var subSubDepartment in subSubDepartments)
                                {
                                    JsTreeJson jsTreeSubSub = new JsTreeJson
                                    {
                                        id = subSubDepartment.DepartmentId + "_ImportedSubSub",
                                        parent = jsTreeSub.id,
                                        text = direction == "ltr" ? subSubDepartment.DepartmentNameEn : subSubDepartment.DepartmentNameAr
                                    };
                                    model.JsTreeJsons.Add(jsTreeSubSub);
                                    ProductSectionsListForTree subSubSectionToAdd = new ProductSectionsListForTree
                                    {
                                        id = subSubDepartment.DepartmentId + "_ImportedSubSub",
                                        parent = jsTreeSub.id,
                                        textEn = subSubDepartment.DepartmentNameEn,
                                        textAr = subSubDepartment.DepartmentNameAr,
                                        DepartmentId = subSubDepartment.DepartmentId
                                    };
                                    model.ProductSectionsChildList.Add(subSubSectionToAdd);
                                }
                            }
                        }
                    }
                }
            }
            return model;
        }

        #endregion

        #endregion

    }
}