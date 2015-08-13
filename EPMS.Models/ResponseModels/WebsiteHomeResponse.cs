using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class WebsiteHomeResponse
    {
        public IEnumerable<WebsiteDepartment> WebsiteDepartments { get; set; }
        public IEnumerable<Partner> Partners { get; set; }
    }
}
