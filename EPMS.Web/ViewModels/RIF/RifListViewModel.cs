using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.RIF
{
    public class RifListViewModel
    {
        //Rfi's Search Request data
        public RifSearchRequest SearchRequest { get; set; }
        /// <summary>
        /// Requests List
        /// </summary>
        public IEnumerable<Models.RIF> aaData { get; set; }
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