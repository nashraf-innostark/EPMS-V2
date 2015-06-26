using System.Collections.Generic;

namespace EPMS.Web.ViewModels.PurchaseOrder
{
    public class PurchaseOrderDetailsViewModel
    {
        public PurchaseOrderDetailsViewModel()
        {
            PurchaseOrder = new Models.PurchaseOrder();
            OrderItems = new List<Models.PurchaseOrderItem>();
            Vendors = new List<Models.Vendor>();
        }
        public IEnumerable<Models.Vendor> Vendors { get; set; }
        public Models.PurchaseOrder PurchaseOrder { get; set; }
        public IList<Models.PurchaseOrderItem> OrderItems { get; set; }
    }
}