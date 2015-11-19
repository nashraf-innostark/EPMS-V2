using System.Collections.Generic;
using EPMS.Models.RequestModels.Reports;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class CustomerServiceListViewModel
    {
        public CustomerServiceListViewModel()
        {
            Reports = new List<Report>();
        }
        public IEnumerable<Report> Reports { get; set; }
        public CustomerServiceReportsSearchRequest SearchRequest { get; set; }

        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int recordsTotal;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int recordsFiltered;
        public string sEcho;
    }
}
