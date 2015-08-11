using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.PurchaseOrder
{
    public class PurchaseOrderDetailsViewModel
    {
        public PurchaseOrderDetailsViewModel()
        {
            PurchaseOrder = new WebsiteModels.PurchaseOrder();
            OrderItems = new List<WebsiteModels.PurchaseOrderItem>();
            Vendors = new List<WebsiteModels.Vendor>();
        }
        public IEnumerable<WebsiteModels.Vendor> Vendors { get; set; }
        public WebsiteModels.PurchaseOrder PurchaseOrder { get; set; }
        public IList<WebsiteModels.PurchaseOrderItem> OrderItems { get; set; }
    }
}