using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class PODetailResponse
    {
        public IEnumerable<Vendor> Vendors { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public IEnumerable<PurchaseOrderItem> OrderItems { get; set; }
    }
}
