using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Website.Department
{
    public class DepartmentViewModel
    {
        public DepartmentViewModel()
        {
            WebsiteDepartment = new WebsiteDepartment();
        }
        public WebsiteDepartment WebsiteDepartment { get; set; }
    }
}