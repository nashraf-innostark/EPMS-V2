using System.Collections.Generic;
using EPMS.Web.Models;
using EPMS.Web.Models.Common;

namespace EPMS.Web.ViewModels.Website.WebsiteServices
{
    public class ServicesListViewModel
    {
        public IList<WebsiteService> WebsiteServices { get; set; }
        public IList<WebsiteService> Services { get; set; }
        public IList<WebsiteService> Sections { get; set; }
        public IList<JsTreeJson> ServicesTree { get; set; }
    }
}