using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.PurchaseOrder
{
    public class PurchaseOrderListViewModel
    {
        public PurchaseOrderSearchRequest SearchRequest { get; set; }
        public IEnumerable<Models.PurchaseOrder> aaData { get; set; }
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