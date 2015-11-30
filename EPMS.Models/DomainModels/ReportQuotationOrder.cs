using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class ReportQuotationOrder
    {
        public long ReportQIId { get; set; }
        public long ReportId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerNameA { get; set; }
        public string CustomerNameE { get; set; }
        public int NoOfRFQ { get; set; }
        public int NoOfOrders { get; set; }
        public bool IsOrdersReport { get; set; }
        public bool IsQuotationsReport { get; set; }
        public bool IsParent { get; set; }
        public long? ParentReportId { get; set; }
        public string TimeStamp { get; set; }
        public string Value { get; set; }

        public virtual ICollection<ReportQuotationOrder> ReportQuotationItems { get; set; }
        public virtual ReportQuotationOrder ReportQuotation { get; set; }
        public virtual Report Report { get; set; }
    }
}
