using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class WebsiteDepartmentResponse
    {
        public WebsiteDepartment websiteDepartment;
        public IEnumerable<ProductSection> ProductSections { get; set; }

    }
}
