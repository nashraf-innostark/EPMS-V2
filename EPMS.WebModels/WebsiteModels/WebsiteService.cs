using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.WebModels.WebsiteModels
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
        public string ImagePath { get; set; }
        public string MetaKeywordsEn { get; set; }
        public string MetaKeywordsAr { get; set; }
        public string MetaDescriptionEn { get; set; }
        public string MetaDescriptionAr { get; set; }
        public bool ShowToPublic { get; set; }
        public long? ParentServiceId { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDate { get; set; }
        public string RecCreatedDateStr { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public long NoofSections { get; set; }
        public long NoofSubSections { get; set; }
        public string ParentServiceEn { get; set; }
        public string ParentServiceAr { get; set; }
        public WebsiteService ParentService { get; set; }
    }
}
