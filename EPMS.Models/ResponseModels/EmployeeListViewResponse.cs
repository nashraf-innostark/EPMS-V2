using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeListViewResponse
    {
        /// <summary>
        /// Job Titles 
        /// </summary>
        public IEnumerable<JobTitle> JobTitles { get; set; }

        /// <summary>
        /// Departments List
        /// </summary>
        public IEnumerable<Department> Departments { get; set; }
    }
}
