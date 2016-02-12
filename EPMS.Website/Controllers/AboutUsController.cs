using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers.Website.AboutUs;
using EPMS.WebModels.ViewModels.AboutUs;
using EPMS.WebModels.ViewModels.Common;

namespace EPMS.Website.Controllers
{
    public class AboutUsController : BaseController
    {
        #region Private

        private readonly IAboutUsService aboutUsService;

        #endregion

        #region Constructor

        public AboutUsController(IAboutUsService aboutUsService)
        {
            this.aboutUsService = aboutUsService;
        }

        #endregion

        #region Public

        public ActionResult Detail()
        {
            AboutUsViewModel aboutUsViewModel = new AboutUsViewModel();
            var aboutus = aboutUsService.GetDetailForWebsite();
            if (aboutus != null)
            {
                aboutUsViewModel.AboutUs = aboutus.CreateFromServerToClient();
            }
            ViewBag.MetaTagsEn = new MetaTagsResponse
            {
                Name = aboutUsViewModel.AboutUs.MetaKeywords,
                Description = aboutUsViewModel.AboutUs.MetaDesc
            };
            ViewBag.MetaTagsAr = new MetaTagsResponse
            {
                Name = aboutUsViewModel.AboutUs.MetaKeywordsAr,
                Description = aboutUsViewModel.AboutUs.MetaDescAr
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(aboutUsViewModel);
        }

        #endregion
    }
}