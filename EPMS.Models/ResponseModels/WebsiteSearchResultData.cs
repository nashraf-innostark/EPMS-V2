using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class WebsiteSearchResultData
    {
        IEnumerable<Product> Products { get; set; }
        IEnumerable<NewsAndArticle> NewsAndArticles { get; set; }
        IEnumerable<WebsiteService> WebsiteServices { get; set; } 
    }
}
