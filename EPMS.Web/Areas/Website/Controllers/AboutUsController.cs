using System;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers.Website.AboutUs;
using EPMS.WebModels.ViewModels.AboutUs;
using EPMS.WebModels.ViewModels.Common;
using Microsoft.AspNet.Identity;
using AboutUs = EPMS.Models.DomainModels.AboutUs;

namespace EPMS.Web.Areas.Website.Controllers
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

        [SiteAuthorize(PermissionKey = "AboutUsDetail")]
        public ActionResult Detail()
        {
            AboutUsViewModel aboutUsViewModel = new AboutUsViewModel();
            var aboutUsDetails = aboutUsService.GetDetail();
            if (aboutUsDetails != null)
            {
                aboutUsViewModel.AboutUs = aboutUsDetails.CreateFromServerToClient();
            }
            return View(aboutUsViewModel);
        }

        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        public ActionResult Detail(AboutUsViewModel aboutUsViewModel)
        {
            try
            {
                if (aboutUsViewModel.AboutUs.AboutUsId > 0)
                {
                    aboutUsViewModel.AboutUs.RecLastUpdatedBy = User.Identity.GetUserId();
                    aboutUsViewModel.AboutUs.RecLastUpdatedDt = DateTime.Now;
                    AboutUs detailToUpdate = aboutUsViewModel.AboutUs.CreateFromClientToServer();
                    if (aboutUsService.UpdateDetail(detailToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.Website.AboutUs.AboutUs.Updated,
                            IsUpdated = true
                        };
                        return RedirectToAction("Detail");
                    }
                }
                else
                {
                    aboutUsViewModel.AboutUs.RecCreatedBy = User.Identity.GetUserId();
                    aboutUsViewModel.AboutUs.RecCreatedDt = DateTime.Now;
                    aboutUsViewModel.AboutUs.RecLastUpdatedBy = User.Identity.GetUserId();
                    aboutUsViewModel.AboutUs.RecLastUpdatedDt = DateTime.Now;
                    AboutUs detailToUpdate = aboutUsViewModel.AboutUs.CreateFromClientToServer();
                    if (aboutUsService.AddDetail(detailToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.Website.AboutUs.AboutUs.Saved,
                            IsSaved = true
                        };
                        return RedirectToAction("Detail");
                    }
                }
            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = e.Message, IsError = true };
                return RedirectToAction("Detail", e);
            }
            return View(aboutUsViewModel);
        }

        #endregion
    }
}