﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class NewsAndArticle
    {
        public long NewsArticleId { get; set; }
        public int? SortOrder { get; set; }
        public bool Type { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Website.NewsAndArticles.NewsAndArticleCreate), ErrorMessageResourceName = "TitleValidation")]
        [StringLength(300, ErrorMessageResourceType = typeof(Resources.Website.NewsAndArticles.NewsAndArticleCreate), ErrorMessageResourceName = "TitleLengthValidation")]
        public string TitleEn { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Website.NewsAndArticles.NewsAndArticleCreate), ErrorMessageResourceName = "TitleValidation")]
        [StringLength(300, ErrorMessageResourceType = typeof(Resources.Website.NewsAndArticles.NewsAndArticleCreate), ErrorMessageResourceName = "TitleLengthValidation")]
        public string TitleAr { get; set; }
        public string ImagePath { get; set; }
        public string AuthorNameEn { get; set; }
        public string AuthorNameAr { get; set; }
        public string ContentEn { get; set; }
        public string ContentAr { get; set; }
        public bool ShowToPublic { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public string DateTimeForIndex { get; set; }
        public string TypeForIndex { get; set; }
    }
}