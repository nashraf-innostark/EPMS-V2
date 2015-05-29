using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.RFI
{
    public class RfiListViewModel
    {
        //Rfi's Search Request data
        public RfiSearchRequest SearchRequest { get; set; }
        /// <summary>
        /// Requests List
        /// </summary>
        public IEnumerable<Models.RFI> aaData { get; set; }
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;
        public int sLimit;
        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;
        public string sEcho;
    }
}