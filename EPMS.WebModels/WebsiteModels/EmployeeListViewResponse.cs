using System.Collections.Generic;

namespace EPMS.WebModels.WebsiteModels
{
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