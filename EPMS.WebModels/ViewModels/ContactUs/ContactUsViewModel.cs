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
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public string ReceiverEmail { get; set; }

    }
}