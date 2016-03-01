using System;
using System.Globalization;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers.Website.ContactUs;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.ContactUs;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Website.Controllers
{
    [SiteAuthorize(PermissionKey = "Website", IsModule = true)]
    public class ContactUsController : BaseController
    {

        #region Private

        private readonly IContactUsService contactUsService;

        #endregion

        #region Constructor

        public ContactUsController(IContactUsService contactUsService)
        {
            this.contactUsService = contactUsService;
        }

        #endregion

        #region Public

        [SiteAuthorize(PermissionKey = "ContactUsDetail")]
        public ActionResult Detail()
        {
            ContactUsViewModel contactUsViewModel = new ContactUsViewModel();
            var contactUsDetails = contactUsService.GetDetail();
            if (contactUsDetails != null)
            {
                contactUsViewModel.ContactUs = contactUsDetails.CreateFromServerToClient();
            }
            return View(contactUsViewModel);
        }

        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor

        public ActionResult Detail(ContactUsViewModel contactUsViewModel)
        {
            try
            {
                if (contactUsViewModel.ContactUs.ContactUsId > 1)
                {
                    contactUsViewModel.ContactUs.RecLastUpdatedBy = User.Identity.GetUserId();
                    contactUsViewModel.ContactUs.RecLastUpdatedDt = DateTime.Now;
                    ContactUs detailToUpdate = contactUsViewModel.ContactUs.CreateFromClientToServer();
                    if (contactUsService.UpdateDetail(detailToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.Website.ContactUs.ContatUs.Updated,
                            IsUpdated = true
                        };
                        return RedirectToAction("Detail");
                    }
                }
                else
                {
                    contactUsViewModel.ContactUs.RecCreatedBy = User.Identity.GetUserId();
                    contactUsViewModel.ContactUs.RecCreatedDt = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
                    contactUsViewModel.ContactUs.RecLastUpdatedBy = User.Identity.GetUserId();
                    contactUsViewModel.ContactUs.RecLastUpdatedDt = DateTime.Now;
                    ContactUs detailToUpdate = contactUsViewModel.ContactUs.CreateFromClientToServer();
                    if (contactUsService.UpdateDetail(detailToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.Website.ContactUs.ContatUs.Saved,
                            IsSaved = true
                        };
                        return RedirectToAction("Detail");
                    }
                }
            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel {Message = e.Message, IsError = true};
                return RedirectToAction("Create", e);
            }
            return View(contactUsViewModel);
        }

        #endregion

    }
}