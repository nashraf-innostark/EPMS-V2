using System.Collections.Generic;
using EPMS.Models.RequestModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class ProjectsReportsListViewModel
    {
        public ProjectsReportsListViewModel()
        {
            aaData = new List<Report>();
        }
        public ProjectReportSearchRequest ProjectReportSearchRequest { get; set; }
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
