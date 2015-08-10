using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class WebsiteService
    {
        public long ServiceId { get; set; }
        [Required]
        [Display(Name = "Service Name")]
        public string ServiceNameEn { get; set; }
        [Required]
        [Display(Name = "Service Name Arabic")]
        public string ServiceNameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string MetaKeywordsEn { get; set; }
        public string MetaKeywordsAr { get; set; }
        public string MetaDescriptionEn { get; set; }
        public string MetaDescriptionAr { get; set; }
        public bool ShowToPublic { get; set; }
        public long? ParentServiceId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecCreatedDateStr { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
    }
}