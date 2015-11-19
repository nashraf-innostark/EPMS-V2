using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class CustomerReportListResponse
    {
        public CustomerReportListResponse()
        {
            Reports = new List<Report>();
        }
        public IEnumerable<Report> Reports { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
}
