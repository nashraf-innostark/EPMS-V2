using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class DepartmentResponse
    {
        public DepartmentResponse()
        {
            Departments = new List<Department>();
        }

        public IEnumerable<Department> Departments { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }

        public Department Department { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
