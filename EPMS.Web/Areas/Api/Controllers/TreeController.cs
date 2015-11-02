using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
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
            IList<WebModels.WebsiteModels.Common.JsTreeJson> details = Utility.InventoryDepartmentTree(departments, direction);
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(details);
            return Json(serializedResult, JsonRequestBehavior.AllowGet);
        }
    }
}