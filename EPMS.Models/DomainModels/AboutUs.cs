using System;

namespace EPMS.Models.DomainModels
{
    public class AboutUs
    {
        public long AboutUsId { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
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
