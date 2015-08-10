using System.Collections.Generic;
using EPMS.Web.Models;
using EPMS.Web.Models.Common;

namespace EPMS.Web.ViewModels.Website.WebsiteServices
{
    public class ServicesCreateViewModel
    {
        public WebsiteService WebsiteService { get; set; }
        public IList<WebsiteService> WebsiteServices { get; set; }
        public IList<JsTreeJson> ServicesTree { get; set; }
    }
}