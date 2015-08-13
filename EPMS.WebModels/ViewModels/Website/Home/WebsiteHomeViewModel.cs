using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Website.Home
{
    public class WebsiteHomeViewModel
    {
        public WebsiteHomeViewModel()
        {
            Partners = new List<WebsiteModels.Partner>();
            WebsiteDepartments = new List<WebsiteDepartment>();
        }
        public IEnumerable<WebsiteDepartment> WebsiteDepartments { get; set; }
        public IEnumerable<WebsiteModels.Partner> Partners { get; set; }
    }
}
