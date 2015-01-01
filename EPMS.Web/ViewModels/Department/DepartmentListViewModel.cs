using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Department
{
    /// <summary>
    /// Department List View Model for Loading List of Departments
    /// </summary>
    public class DepartmentListViewModel
    {
        public DepartmentListViewModel()
        {
            Department = new Models.Department();
        }
        public Models.Department Department { get; set; }
        public IEnumerable<Models.Department> DepartmentList { get; set; }

        public IEnumerable<Models.Employee> EmployeeList { get; set; }
        public DepartmentSearchRequest SearchRequest { get; set; }
        
    }
}