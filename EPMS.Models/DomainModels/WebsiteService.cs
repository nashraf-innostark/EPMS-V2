using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class WebsiteService
    {
        public long ServiceId { get; set; }
        public string ServiceNameEn { get; set; }
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
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public virtual ICollection<WebsiteService> WebsiteServices { get; set; }
        public virtual WebsiteService ParentService { get; set; }
    }
}
