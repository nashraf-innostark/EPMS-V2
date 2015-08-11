using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.PurchaseOrder
{
    public class POHistoryViewModel
    {
        public POHistoryViewModel()
        {
            PurchaseOrders = new List<WebsiteModels.PurchaseOrder>();
            RecentPo = new WebsiteModels.PurchaseOrder();
            PurchaseOrdersItems = new List<WebsiteModels.PurchaseOrderItem>();
            Vendors = new List<WebsiteModels.Vendor>();
        }
        public IList<WebsiteModels.PurchaseOrder> PurchaseOrders { get; set; }
        public WebsiteModels.PurchaseOrder RecentPo { get; set; }
        public IList<WebsiteModels.PurchaseOrderItem> PurchaseOrdersItems { get; set; }
        public IEnumerable<WebsiteModels.Vendor> Vendors { get; set; }
    }
}