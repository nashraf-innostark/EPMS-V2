using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class QuotationInvoiceReportResponse
    {
        public long ReportId { get; set; }
        public int QuotationsCount { get; set; }
        public int InvoicesCount { get; set; }
    }
}
