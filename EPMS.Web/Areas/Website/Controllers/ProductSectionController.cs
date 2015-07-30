using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Website.ProductSection;
using EPMS.Web.Models.Common;
using EPMS.Web.ViewModels.ProductSection;

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
            return View(new ProductSectionListViewModel
            {
                ProductSections = productSectionService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        #endregion

        #region Create

        public ActionResult Create()
        {
            ProductSectionViewModel viewModel = new ProductSectionViewModel();
            var productSections = productSectionService.GetAll().Select(x => x.CreateFromServerToClientForTree());
            string direction = Resources.Shared.Common.TextDirection;
            IList<JsTreeJson> jsTreeJson = new List<JsTreeJson>();
            foreach (var productSection in productSections)
            {
                JsTreeJson jsTree = new JsTreeJson
                    {
                        id = productSection.InventoyDepartmentId == null ? productSection.SectionId.ToString() : productSection.SectionId + "_Imported",
                        parent = productSection.ParentSectionId != null ? productSection.ParentSectionId.ToString() : "#",
                        text = direction == "ltr" ? productSection.InventoryDepartmentNameEn : productSection.InventoryDepartmentNameAr
                    };
                jsTreeJson.Add(jsTree);
                if (productSection.InventoryDepartment != null)
                {
                    if (productSection.InventoryDepartment.InventoryDepartments.Any())
                    {
                        var subDepartments = productSection.InventoryDepartment.InventoryDepartments;
                        foreach (var subDepartment in subDepartments)
                        {
                            JsTreeJson jsTreeSub = new JsTreeJson
                            {
                                id = subDepartment.DepartmentId + "_ImportedSub",
                                parent = jsTree.id,
                                text = direction == "ltr" ? subDepartment.DepartmentNameEn : subDepartment.DepartmentNameAr
                            };
                            jsTreeJson.Add(jsTreeSub);
                            if (subDepartment.InventoryDepartments.Any())
                            {
                                var subSubDepartments = subDepartment.InventoryDepartments;
                                foreach (var subSubDepartment in subSubDepartments)
                                {
                                    JsTreeJson jsTreeSubSub = new JsTreeJson
                                    {
                                        id = subSubDepartment.DepartmentId + "_ImportedSubSub",
                                        parent = jsTreeSub.id,
                                        text = direction == "ltr" ? subSubDepartment.DepartmentNameEn : subSubDepartment.DepartmentNameAr
                                    };
                                    jsTreeJson.Add(jsTreeSubSub);
                                }
                            }
                        }
                    }
                }
            }
            var serializer = new JavaScriptSerializer();
            ViewBag.JsTree = serializer.Serialize(jsTreeJson);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(ProductSectionViewModel viewModel)
        {
            return View();
        }

        #endregion

        #region Delete
        #endregion

        #endregion

    }
}