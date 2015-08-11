using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.JobTitle
{
    public class JobTitleViewModel
    {
        public JobTitleViewModel()
        {
            JobTitle=new WebsiteModels.JobTitle();
        }
        public WebsiteModels.JobTitle JobTitle { get; set; }
        public WebsiteModels.Department Department { get; set; }
        
        public IEnumerable<WebsiteModels.Department> DepartmentList { get; set; }
        public IEnumerable<WebsiteModels.Employee> EmployeeList { get; set; }
        public long SelectedDepartment { get; set; }
    }
}