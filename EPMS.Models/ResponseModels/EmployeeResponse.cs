using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class EmployeeResponse
    {
        public EmployeeResponse()
        {
            Employeess = new List<Employee>();
        }

        public IEnumerable<Employee> Employeess { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}
