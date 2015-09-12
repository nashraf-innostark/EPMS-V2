using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class WebsiteHomeResponse
    {
        public IEnumerable<WebsiteDepartment> WebsiteDepartments { get; set; }
        public IEnumerable<Partner> Partners { get; set; }
    }
}
