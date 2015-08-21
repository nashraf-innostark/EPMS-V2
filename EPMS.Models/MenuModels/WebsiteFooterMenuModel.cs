using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.MenuModels
{
    public class WebsiteFooterMenuModel
    {
        public IEnumerable<NewsAndArticle> NewsAndArticles { get; set; }
        public ContactUs ContactUs { get; set; }
    }
}
