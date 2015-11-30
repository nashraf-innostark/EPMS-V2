using System.Collections.Generic;
using EPMS.Models.RequestModels.Reports;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class TasksReportListViewModel
    {
        public TasksReportListViewModel()
        {
            aaData = new List<Report>();
        }
        public TaskReportSearchRequest TaskReportSearchRequest { get; set; }
        public List<Report> aaData { get; set; }
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;

        public string sEcho;
    }
}
