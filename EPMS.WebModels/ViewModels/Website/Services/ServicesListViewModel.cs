using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels.Common;

namespace EPMS.WebModels.ViewModels.Website.Services
{
    public class ServicesListViewModel
    {
        public IList<WebsiteModels.WebsiteService> WebsiteServices { get; set; }
        public IList<WebsiteModels.WebsiteService> Services { get; set; }
        public IList<WebsiteModels.WebsiteService> Sections { get; set; }
        public IList<JsTreeJson> ServicesTree { get; set; }
    }
}
