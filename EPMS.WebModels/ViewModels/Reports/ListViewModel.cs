using System.Collections.Generic;
using EPMS.Models.RequestModels.Reports;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class ListViewModel
    {
        public ListViewModel()
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
    public class WarehouseReportsListViewModel
    {
        public WarehouseReportsListViewModel()
        {
            aaData = new List<Report>();
        }
        public WarehouseReportSearchRequest WarehouseReportSearchRequest { get; set; }
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
