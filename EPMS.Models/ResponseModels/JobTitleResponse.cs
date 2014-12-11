using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class JobTitleResponse
    {
        public JobTitleResponse()
        {
            JobTitles = new List<JobTitle>();
        }

        public IEnumerable<JobTitle> JobTitles { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }

        //public string DepartmentName { get; set; }
    }
}
