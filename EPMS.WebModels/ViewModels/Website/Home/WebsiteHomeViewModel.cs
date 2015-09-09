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
        public IEnumerable<Models.DomainModels.ProductSection> ProductSections { get; set; }
        public string Div { get; set; }
        public string Width { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
