using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Payroll
{
    public class PayrollViewModel
    {
        public IEnumerable<Models.Payroll> aaData { get; set; }
        public PayrollSearchRequest SearchRequest { get; set; }
    }
}