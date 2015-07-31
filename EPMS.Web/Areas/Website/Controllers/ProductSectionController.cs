using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Website.ProductSection;
using EPMS.Web.Models;
using EPMS.Web.Models.Common;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.ProductSection;
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
            ProductSectionViewModel viewModel = new ProductSectionViewModel();
            var productSections = productSectionService.GetAll().Select(x => x.CreateFromServerToClientForTree());
            string direction = Resources.Shared.Common.TextDirection;
            IList<JsTreeJson> jsTreeJson = new List<JsTreeJson>();
            viewModel.ProductSections = new List<ProductSection>();
            viewModel.ProductSectionsChildList = new List<ProductSectionsListForTree>();
            foreach (var productSection in productSections)
            {
                // add root node
                JsTreeJson jsTree;
                if (productSection.InventoyDepartmentId != null)
                {
                    jsTree = new JsTreeJson
                    {
                        id = productSection.SectionId + "_Imported",
                        parent = productSection.ParentSectionId != null ? productSection.ParentSectionId.ToString() : "#",
                        text = direction == "ltr" ? productSection.InventoryDepartmentNameEn : productSection.InventoryDepartmentNameAr
                    };
                }
                else
                {
                    jsTree = new JsTreeJson
                    {
                        id = productSection.SectionId.ToString(),
                        parent = productSection.ParentSectionId != null ? productSection.ParentSectionId.ToString() : "#",
                        text = direction == "ltr" ? productSection.SectionNameEn : productSection.SectionNameAr
                    };
                }
                jsTreeJson.Add(jsTree);
                viewModel.ProductSections.Add(productSection);
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
                            jsTreeJson.Add(jsTreeSub);
                            ProductSectionsListForTree subSectionToAdd = new ProductSectionsListForTree
                            {
                                id = subDepartment.DepartmentId + "_ImportedSub",
                                parent = jsTree.id,
                                textEn = subDepartment.DepartmentNameEn,
                                textAr = subDepartment.DepartmentNameAr,
                                DepartmentId = subDepartment.DepartmentId
                            };
                            viewModel.ProductSectionsChildList.Add(subSectionToAdd);
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
                                    jsTreeJson.Add(jsTreeSubSub);
                                    ProductSectionsListForTree subSubSectionToAdd = new ProductSectionsListForTree
                                    {
                                        id = subSubDepartment.DepartmentId + "_ImportedSubSub",
                                        parent = jsTreeSub.id,
                                        textEn = subSubDepartment.DepartmentNameEn,
                                        textAr = subSubDepartment.DepartmentNameAr,
                                        DepartmentId = subSubDepartment.DepartmentId
                                    };
                                    viewModel.ProductSectionsChildList.Add(subSubSectionToAdd);
                                }
                            }
                        }
                    }
                }
            }
            // Javascript Serializer
            var serializer = new JavaScriptSerializer();
            ViewBag.JsTree = serializer.Serialize(jsTreeJson);
            // use new version of JsTree
            ViewBag.IsIncludeNewJsTree = true;
            // use new version of jQuery switch
            ViewBag.IsIncludeNewSwitch = true;
            ViewBag.ProductId = id;
            return View(viewModel);
        }

        [HttpPost]
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

        #endregion

    }
}