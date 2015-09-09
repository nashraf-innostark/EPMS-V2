using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.NewsAndArticle
{
    public class NewsAndArticleListViewModel
    {
        public IEnumerable<WebsiteModels.NewsAndArticle> NewsAndArticles { get; set; }
        public bool NewsOrArticle { get; set; }
        public NewsAndArticleSearchRequest SearchRequest { get; set; }
        public long Type { get; set; }
    }
}