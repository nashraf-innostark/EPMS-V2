using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Website.Services
{
    public class ServicesViewModel
    {
        public IList<WebsiteModels.WebsiteService> WebsiteServices { get; set; }
        public IList<WebsiteModels.WebsiteService> Services { get; set; }
        public IList<WebsiteModels.WebsiteService> Sections { get; set; }
        public IList<WebsiteModels.Common.JsTreeJson> ServicesTree { get; set; }
    }
}