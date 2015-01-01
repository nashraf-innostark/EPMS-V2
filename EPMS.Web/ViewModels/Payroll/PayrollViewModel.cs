using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Payroll
{
    public class PayrollViewModel
    {
        public IEnumerable<Models.Payroll> Requests { get; set; }
        public Models.Employee Employee { get; set; }

        public PayrollSearchRequest SearchRequest { get; set; }
    }
}