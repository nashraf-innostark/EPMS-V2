using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.PhysicalCount
{
    public class PhysicalCountListViewModel
    {
        public IEnumerable<WebsiteModels.PhysicalCountListModel> aaData { get; set; }
        public PhysicalCountSearchRequest SearchRequest { get; set; }

        public string sEcho;
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;
        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;
    }
}