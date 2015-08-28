using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.MenuModels
{
    public class WebsiteFooterMenuModel
    {
        public WebsiteFooterMenuModel()
        {
            NewsAndArticles = new List<NewsAndArticle>();
            ContactUs = new ContactUs();
        }
        public IEnumerable<NewsAndArticle> NewsAndArticles { get; set; }
        public ContactUs ContactUs { get; set; }
    }
}
