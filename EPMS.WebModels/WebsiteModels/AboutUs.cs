using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class AboutUs
    {
        public long AboutUsId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Website.AboutUs.AboutUs), ErrorMessageResourceName = "TitleValidErrorEn")]
        public string Title { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Website.AboutUs.AboutUs), ErrorMessageResourceName = "TitleValidErrorAr")]
        public string TitleAr { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Website.AboutUs.AboutUs), ErrorMessageResourceName = "ContentValidErrorEn")]
        public string ContentEn { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Website.AboutUs.AboutUs), ErrorMessageResourceName = "ContentValidErrorAr")]
        public string ContentAr { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaKeywordsAr { get; set; }
        public string MetaDesc { get; set; }
        public string MetaDescAr { get; set; }
        public bool ShowToPublic { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
    }
}
