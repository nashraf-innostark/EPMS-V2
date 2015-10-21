using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class WebsiteSearchResultData
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<NewsAndArticle> NewsAndArticles { get; set; }
        public IEnumerable<WebsiteService> WebsiteServices { get; set; } 
    }
}
