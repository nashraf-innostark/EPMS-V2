using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class WebsiteServicesCreateResponse
    {
        public WebsiteService WebsiteService { get; set; }
        public IEnumerable<WebsiteService> WebsiteServices { get; set; }
    }
}