using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class PoHistoryResponse
    {
        public string RequesterNameEn { get; set; }
        public string RequesterNameAr { get; set; }
        public string ManagerNameEn { get; set; }
        public string ManagerNameAr { get; set; }
        public PurchaseOrder RecentPo { get; set; }
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; }
        public IEnumerable<PurchaseOrderItem> PoItems { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
    }
}
