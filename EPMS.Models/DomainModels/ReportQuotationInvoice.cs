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
        public long EmployeeId { get; set; }
        public string EmployeeNameA { get; set; }
        public string EmployeeNameE { get; set; }
        public int NoOfQuotations { get; set; }
        public int NoOfInvoices { get; set; }

        public virtual ICollection<ReportQuotationInvoiceItem> ReportQuotationInvoiceItems { get; set; }
        public virtual Report Report { get; set; }
    }
}
