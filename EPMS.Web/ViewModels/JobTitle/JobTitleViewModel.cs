using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.JobTitle
{
    public class JobTitleViewModel
    {
        public JobTitleViewModel()
        {
            JobTitle=new Models.JobTitle();
        }
        public Models.JobTitle JobTitle { get; set; }
        public Models.Department Department { get; set; }
        
        public IEnumerable<Models.Department> DepartmentList { get; set; }
        public IEnumerable<Models.Employee> EmployeeList { get; set; }
        public long SelectedDepartment { get; set; }
    }
}