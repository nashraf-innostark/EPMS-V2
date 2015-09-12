using System.Web.Mvc;
using EPMS.Interfaces.IServices;
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
            ViewBag.ShowSlider = false;
            var aboutus = aboutUsService.GetDetail();
            if (aboutus != null)
            {
                aboutUsViewModel.AboutUs = aboutus.CreateFromServerToClient();
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(aboutUsViewModel);
        }

        #endregion
    }
}