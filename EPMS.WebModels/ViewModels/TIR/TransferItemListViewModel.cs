using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.TIR
{
    public class TransferItemListViewModel
    {
        public TransferItemSearchRequest SearchRequest { get; set; }
        public IEnumerable<EPMS.WebModels.WebsiteModels.TIR> aaData { get; set; }
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