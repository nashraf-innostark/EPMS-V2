using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string Message { get; set; }
        public string ReceiverEmail { get; set; }

    }
}