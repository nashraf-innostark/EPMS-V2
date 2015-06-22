using System.Collections.Generic;

namespace EPMS.Web.ViewModels.PurchaseOrder
{
    public class PurchaseOrderDetailsViewModel
    {
        public Models.PurchaseOrder PurchaseOrder { get; set; }
        public IEnumerable<Models.PurchaseOrderItem> OrderItems { get; set; }
    }
}