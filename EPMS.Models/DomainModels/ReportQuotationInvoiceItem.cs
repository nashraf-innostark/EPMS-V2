using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class ReportQuotationInvoiceItem
    {
        public long ReportQuotationInvoiceItemId { get; set; }
        public long ReportQuotationInvoiceId { get; set; }
        public string MonthTimeStamp { get; set; }
        public string TotalPrice { get; set; }
        public bool IsInvoiceItem { get; set; }
        public bool IsQuotationItem { get; set; }

        public virtual ReportQuotationInvoice ReportQuotationInvoice { get; set; }
    }
}
