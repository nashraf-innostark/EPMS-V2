using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class ContactUs
    {
        public long ContactUsId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Website.ContactUs.ContatUs), ErrorMessageResourceName = "TitleValidation")]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.Website.ContactUs.ContatUs), ErrorMessageResourceName = "TitleLengthValidation")]
        public string Title { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Website.ContactUs.ContatUs), ErrorMessageResourceName = "TitleValidation")]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.Website.ContactUs.ContatUs), ErrorMessageResourceName = "TitleLengthValidation")]
        public string TitleAr { get; set; }
        public string Address { get; set; }
        public string AddressAr { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string POBox { get; set; }
        public string Website { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FormEmail { get; set; }
        public string ContentEn { get; set; }
        public string ContentAr { get; set; }
        public bool ShowToPublic { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
    }
}