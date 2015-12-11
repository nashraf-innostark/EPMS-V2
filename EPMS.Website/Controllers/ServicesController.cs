using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers.Website.Services;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Website.Services;

namespace EPMS.Website.Controllers
{
    public class ServicesController : BaseController
    {
        private readonly IWebsiteServicesService websiteServicesService;

        #region Private
        #endregion

        #region Constructor

        public ServicesController(IWebsiteServicesService websiteServicesService)
        {
            this.websiteServicesService = websiteServicesService;
        }

        #endregion

        #region Public

        public ActionResult Detail(long id)
        {
            ServicesCreateViewModel viewmodel = new ServicesCreateViewModel
            {
                WebsiteService = websiteServicesService.FindWebsiteServiceById(id).CreateFromServerToClient()
            };
            ViewBag.MetaTagsEn = new MetaTagsResponse
            {
                Name = viewmodel.WebsiteService.MetaKeywordsEn,
                Description = viewmodel.WebsiteService.MetaDescriptionEn
            };
            ViewBag.MetaTagsAr = new MetaTagsResponse
            {
                Name = viewmodel.WebsiteService.MetaKeywordsAr,
                Description = viewmodel.WebsiteService.MetaDescriptionAr
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewmodel);
        }

        #endregion
    }
}