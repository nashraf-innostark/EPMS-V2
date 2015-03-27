using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class EmployeeResponse
    {
        public EmployeeResponse()
        {
            Employeess = new List<Employee>();
            JobHistories = new List<JobHistory>();
        }

        public IEnumerable<Employee> Employeess { get; set; }
        public Employee Employee { get; set; }
        public Allowance Allowance { get; set; }
        public IList<JobHistory> JobHistories { get; set; }
        public IEnumerable<JobTitle> JobTitleList { get; set; }
        public IEnumerable<EmployeeRequest> EmployeeMonetaryRequests { get; set; }
        public IEnumerable<EmployeeRequest> EmployeeRequests { get; set; }
        public IEnumerable<TaskEmployee> EmployeeTasks { get; set; }
        //public EmployeeJobHistoryResponse JobHistory { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}
