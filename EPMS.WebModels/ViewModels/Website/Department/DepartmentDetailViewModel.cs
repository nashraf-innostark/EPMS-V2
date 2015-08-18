using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Website.Department
{
    public class DepartmentDetailViewModel
    {
        public DepartmentDetailViewModel()
        {
            WebsiteDepartment = new WebsiteDepartment();
        }
        public WebsiteDepartment WebsiteDepartment { get; set; }
        public IEnumerable<Models.DomainModels.ProductSection> ProductSections { get; set; }
    }
}
