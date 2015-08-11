namespace EPMS.WebModels.ViewModels.Website.Department
{
    public class DepartmentViewModel
    {
        public DepartmentViewModel()
        {
            WebsiteDepartment = new WebsiteModels.WebsiteDepartment();
        }
        public WebsiteModels.WebsiteDepartment WebsiteDepartment { get; set; }
    }
}