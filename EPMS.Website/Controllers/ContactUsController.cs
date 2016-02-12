using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.ContactUs;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.ContactUs;

namespace EPMS.Website.Controllers
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
            var contactUs = contactUsService.GetDetailForWebsite();
            if (contactUs.ShowToPublic)
            {
                contactUsViewModel.ContactUs = contactUs.CreateFromServerToClient();
            }
            contactUsViewModel.ReceiverEmail = contactUs.FormEmail;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(contactUsViewModel);
        }

        public ActionResult SendEmail(ContactUsViewModel contactUsViewModel)
        {

            var email = contactUsViewModel.ReceiverEmail;
            const string subject = "Contact Us Form";
            var body = "Name: " + contactUsViewModel.Name + "<br/>" + "Email: " + contactUsViewModel.Email + "<br/>" + "Address: " + contactUsViewModel.Address + "<br/>" + "Message: " + contactUsViewModel.Message;
            if (email != null || email != "")
            {
                Utility.SendEmailAsync(email, subject, body);
            }
            return RedirectToAction("Detail");
        }

        #endregion
    }
}