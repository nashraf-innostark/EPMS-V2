using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;

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
    }
}