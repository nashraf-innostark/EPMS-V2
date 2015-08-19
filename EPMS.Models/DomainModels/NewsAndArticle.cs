using System;

namespace EPMS.Models.DomainModels
{
    public class NewsAndArticle
    {
        public long NewsArticleId { get; set; }
        public int? SortOrder { get; set; }
        public bool Type { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string ImagePath { get; set; }
        public string AuthorNameEn { get; set; }
        public string AuthorNameAr { get; set; }
        public string ContentEn { get; set; }
        public string ContentAr { get; set; }
        public bool ShowToPublic { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaKeywordsAr { get; set; }
        public string MetaDesc { get; set; }
        public string MetaDescAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
    }
}
