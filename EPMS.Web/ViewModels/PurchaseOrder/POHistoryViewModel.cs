using System.Collections.Generic;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.PurchaseOrder
{
    public class POHistoryViewModel
    {
        public POHistoryViewModel()
        {
            PurchaseOrders = new List<Models.PurchaseOrder>();
            RecentPo = new Models.PurchaseOrder();
            PurchaseOrdersItems = new List<PurchaseOrderItem>();
            Vendors = new List<Models.Vendor>();
        }
        public IList<Models.PurchaseOrder> PurchaseOrders { get; set; }
        public Models.PurchaseOrder RecentPo { get; set; }
        public IList<PurchaseOrderItem> PurchaseOrdersItems { get; set; }
        public IEnumerable<Models.Vendor> Vendors { get; set; }
    }
}