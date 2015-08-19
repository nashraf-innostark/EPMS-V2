using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels.Common;

namespace EPMS.WebModels.ViewModels.Website.Services
{
    public class ServicesCreateViewModel
    {
        public WebsiteModels.WebsiteService WebsiteService { get; set; }
        public IList<WebsiteModels.WebsiteService> WebsiteServices { get; set; }
        public IList<JsTreeJson> ServicesTree { get; set; }
    }
}