using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class TaskReportsListRequestResponse
    {
        public TaskReportsListRequestResponse()
        {
            Tasks = new List<Report>();
        }
        public IEnumerable<Report> Tasks { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
}
