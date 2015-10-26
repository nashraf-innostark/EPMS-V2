using System.Collections.Generic;
using EPMS.Models.RequestModels;
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
            NewsAndArticleSearchRequest = new NewsAndArticleSearchRequest();
            ProductSearchRequest = new ProductSearchRequest();
            WebsiteServiceSearchRequest = new WebsiteServiceSearchRequest();
        }
        public List<WebModel.Product> Products { get; set; }
        public ProductSearchRequest ProductSearchRequest { get; set; }
        public List<WebModel.NewsAndArticle> NewsAndArticles { get; set; }
        public NewsAndArticleSearchRequest NewsAndArticleSearchRequest { get; set; }
        public List<WebModel.WebsiteService> WebsiteServices { get; set; }
        public WebsiteServiceSearchRequest WebsiteServiceSearchRequest { get; set; }
        public WebModel.AboutUs AboutUs { get; set; }
        public WebModel.ContactUs ContactUs { get; set; }

        public string searchText { get; set; }
    }
}
