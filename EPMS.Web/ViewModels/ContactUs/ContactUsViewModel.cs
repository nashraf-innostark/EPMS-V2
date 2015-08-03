namespace EPMS.Web.ViewModels.ContactUs
{
    public class ContactUsViewModel
    {
        #region Constructor

        public ContactUsViewModel()
        {
            ContactUs = new Models.ContactUs();
        }

        #endregion
        public Models.ContactUs ContactUs { get; set; }

    }
}