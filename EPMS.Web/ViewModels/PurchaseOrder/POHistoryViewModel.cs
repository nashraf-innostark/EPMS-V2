using System.Collections.Generic;

namespace EPMS.Web.ViewModels.PurchaseOrder
{
    public class POHistoryViewModel
    {
        public IList<Models.PurchaseOrder> PurchaseOrders { get; set; }
        public Models.PurchaseOrder RecentPo { get; set; }
        public IList<Models.PurchaseOrderItem> PurchaseOrdersItems { get; set; }
        public IEnumerable<Models.Vendor> Vendors { get; set; }
    }
}