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
            ViewBag.MetaKeywordsEn = aboutUsViewModel.AboutUs.MetaKeywords;
            ViewBag.MetaKeywordsAr = aboutUsViewModel.AboutUs.MetaKeywordsAr;
            ViewBag.MetaDescriptionEn = aboutUsViewModel.AboutUs.MetaKeywords;
            ViewBag.MetaDescriptionAr = aboutUsViewModel.AboutUs.MetaKeywordsAr;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(aboutUsViewModel);
        }

        #endregion
    }
}