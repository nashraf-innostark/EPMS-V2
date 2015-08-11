namespace EPMS.WebModels.ViewModels.ContactUs
{
    public class ContactUsViewModel
    {
        #region Constructor

        public ContactUsViewModel()
        {
            ContactUs = new WebsiteModels.ContactUs();
        }

        #endregion
        public WebsiteModels.ContactUs ContactUs { get; set; }

    }
}