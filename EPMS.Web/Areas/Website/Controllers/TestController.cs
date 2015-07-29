using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.Models.Common;
using System.Linq;
using EPMS.Web.ModelMappers;
using Newtonsoft.Json;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class TestController : BaseController
    {
        #region Private

        private readonly IInventoryDepartmentService departmentService;

        #endregion

        #region Constructor

        public TestController(IInventoryDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        #endregion

        // GET: Website/Test
        public ActionResult JsTree()
        {
            return View();
        }
        // GET: Website/Test
        public ActionResult Tree()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetTreeData(long? id)
        {
            var departments = departmentService.GetAll();
            IList<JsTreeJson> details = new List<JsTreeJson>();
            foreach (var inventoryDepartment in departments)
            {
                details.Add(inventoryDepartment.CreateForJsTreeJson());
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
                                    id = itemVariation.ItemVariationId.ToString(),
                                    text = itemVariation.SKUDescriptionEn,
                                    parent = inventoryDepartment.DepartmentId.ToString()
                                };
                                details.Add(item);
                            }
                        }
                    }
                }
            }
            //details = departments.Select(x => x.CreateForJsTreeJson()).ToList();
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(details);
            return Json(serializedResult, JsonRequestBehavior.AllowGet);
        }

    }
}