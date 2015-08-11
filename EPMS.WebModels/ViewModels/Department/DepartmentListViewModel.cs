using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.Department
{
    /// <summary>
    /// Department List View Model for Loading List of Departments
    /// </summary>
    public class DepartmentListViewModel
    {
        public DepartmentListViewModel()
        {
            Department = new WebsiteModels.Department();
        }
        public WebsiteModels.Department Department { get; set; }
        public IEnumerable<WebsiteModels.Department> DepartmentList { get; set; }

        public IEnumerable<WebsiteModels.Employee> EmployeeList { get; set; }
        public DepartmentSearchRequest SearchRequest { get; set; }
        
    }
}