using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.PurchaseOrder
{
    public class PurchaseOrderListViewModel
    {
        public PurchaseOrderSearchRequest SearchRequest { get; set; }
        public IEnumerable<WebsiteModels.PurchaseOrder> aaData { get; set; }
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