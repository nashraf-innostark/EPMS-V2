using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class CustomerReportResponse
    {
        public IEnumerable<Customer> Customers { get; set; }
        public long ReportId { get; set; }
    }
}
