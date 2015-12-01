using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class ReportQuotationInvoice
    {
        public long ReportQuotationInvoiceId { get; set; }
        public long ReportId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerNameA { get; set; }
        public string CustomerNameE { get; set; }
        public int NoOfQuotations { get; set; }
        public int NoOfInvoices { get; set; }

        public virtual ICollection<ReportQuotationInvoiceItem> ReportQuotationInvoiceItems { get; set; }
    }
}
