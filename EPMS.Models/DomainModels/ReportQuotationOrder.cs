using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class ReportQuotationOrder
    {
        public long ReportQuotOrderId { get; set; }
        public long ReportId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerNameA { get; set; }
        public string CustomerNameE { get; set; }
        public int NoOfRFQ { get; set; }
        public int NoOfOrders { get; set; }

        public virtual Report Report { get; set; }
        public virtual ICollection<ReportQuotationOrderItem> ReportQuotationOrderItems { get; set; }
    }
}
