using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models.Common;

namespace EPMS.Web.Areas.Api.Controllers
{
    public class TreeController : BaseController
    {
        #region Private

        private readonly IInventoryDepartmentService departmentService;

        #endregion

        #region Constructor
        public TreeController(IInventoryDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        #endregion

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
                                    text = inventoryItem.ItemCode + " - " + itemVariation.SKUCode,
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
    }
}