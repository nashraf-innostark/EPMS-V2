using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.IRF
{
    public class ItemReleaseListViewModel
    {
        public IEnumerable<Models.ItemRelease> aaData { get; set; }
        public ItemReleaseSearchRequest SearchRequest { get; set; }
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