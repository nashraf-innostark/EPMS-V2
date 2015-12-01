using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class ReportQuotationOrderItem
    {
        public long ReportQuotOrderItemId { get; set; }
        public long ReportQuotOrderId { get; set; }
        public string MonthTimeStamp { get; set; }
        public string TotalPrice { get; set; }
        public bool IsOrderReport { get; set; }
        public bool IsQuotationReport { get; set; }

        public virtual ReportQuotationOrder ReportQuotationOrder { get; set; }
    }
}
