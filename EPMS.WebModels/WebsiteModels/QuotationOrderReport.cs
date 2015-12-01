using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.WebsiteModels
{
    public class QuotationOrderReport
    {
        public string CustomerNameA { get; set; }
        public string CustomerNameE { get; set; }
        public int NoOfRFQ { get; set; }
        public int NoOfOrders { get; set; }
        public string ReportFromDateString { get; set; }
        public string ReportToDateString { get; set; }
        public virtual ICollection<ReportQuotationOrderItem> ReportQuotationOrderItems { get; set; }
    }
}
