﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class WebsiteSearchResultData
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<NewsAndArticle> NewsAndArticles { get; set; }
        public IEnumerable<WebsiteService> WebsiteServices { get; set; }
        public NewsAndArticleResponse NewsAndArticleResponse { get; set; }
        public WebsiteSearchResponse WebsiteSearchResponse { get; set; }
        public ProductResponse ProductResponse { get; set; }
        public AboutUs AboutUs { get; set; }
        public ContactUs ContactUs { get; set; }
    }
}