using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers;

namespace EPMS.Web.Areas.Api.Controllers
{
    public class ItemVariationController : Controller
    {
        #region Private

        private readonly IItemVariationService variationService;

        #endregion

        #region Constructor
        public ItemVariationController(IItemVariationService variationService)
        {
            this.variationService = variationService;
        }

        #endregion

        [HttpGet]
        public JsonResult GetItemVariationDetail(long variationId)
        {
            var detail = variationService.GetItemVariationDetail(variationId);
            return Json(detail, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllSizes()
        {
            var sizes = variationService.GetAllSizes().Select(x=>x.CreateForSizeList());
            var serializer = new JavaScriptSerializer();
            var serializedSizes = serializer.Serialize(sizes);
            return Json(serializedSizes, JsonRequestBehavior.AllowGet);
        }
    }
}