using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Website.ContactUs;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.ContactUs;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Website.Controllers
{
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
                        TempData["message"] = new MessageViewModel { Message = Resources.Website.ContactUs.ContatUs.Updated, IsUpdated = true };
                        return RedirectToAction("Detail");
                    }
                }
                else
                {
                    contactUsViewModel.ContactUs.RecCreatedBy = User.Identity.GetUserId();
                    contactUsViewModel.ContactUs.RecCreatedDt = DateTime.Now;
                    contactUsViewModel.ContactUs.RecLastUpdatedBy = User.Identity.GetUserId();
                    contactUsViewModel.ContactUs.RecLastUpdatedDt = DateTime.Now;
                    ContactUs detailToUpdate = contactUsViewModel.ContactUs.CreateFromClientToServer();
                    if (contactUsService.UpdateDetail(detailToUpdate))
                    {
                        TempData["message"] = new MessageViewModel { Message = Resources.Website.ContactUs.ContatUs.Saved, IsUpdated = true };
                        return RedirectToAction("Detail");
                    }
                }
            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = e.Message, IsError = true };
                return RedirectToAction("Create", e);
            }
            return View(contactUsViewModel);
        }

        #endregion

    }
}