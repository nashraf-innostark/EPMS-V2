using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.WebModels.ViewModels.Website.Footer
{
    public class FooterViewModel
    {
        public IEnumerable<WebsiteModels.NewsAndArticle> NewsAndArticles { get; set; }
        public WebsiteModels.ContactUs ContactUs { get; set; }
    }
}
