using System.Collections.Generic;
using WebModel=EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Search
{
    public class SearchResult
    {
        public SearchResult()
        {
            Products=new List<WebModel.Product>();
            NewsAndArticles=new List<WebModel.NewsAndArticle>();
            WebsiteServices=new List<WebModel.WebsiteService>();
        }
        public List<WebModel.Product> Products { get; set; }
        public List<WebModel.NewsAndArticle> NewsAndArticles { get; set; }
        public List<WebModel.WebsiteService> WebsiteServices { get; set; } 
        public WebModel.AboutUs AboutUs { get; set; }
        public WebModel.ContactUs ContactUs { get; set; } 
    }
}
