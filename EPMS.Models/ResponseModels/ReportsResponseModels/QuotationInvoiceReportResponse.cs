using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;
using Employee = EPMS.Models.DashboardModels.Employee;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class QuotationInvoiceReportResponse
    {
        public IList<Employee> Employees { get; set; }
        public long ReportId { get; set; }
        public int QuotationsCount { get; set; }
        public int InvoicesCount { get; set; }
        public string EmployeeNameE { get; set; }
        public string EmployeeNameA { get; set; }
        public IEnumerable<Quotation> Quotations { get; set; }
        public string EndDate { get; set; }
        public string StartDate { get; set; }
    }
}
