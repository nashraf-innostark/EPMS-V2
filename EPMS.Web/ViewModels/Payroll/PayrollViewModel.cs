using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Payroll
{
    public class PayrollViewModel
    {
        public PayrollViewModel()
        {
            Payroll = new Models.Payroll();
        }
        //public IEnumerable<Models.Payroll> Requests { get; set; }
        public IEnumerable<Models.RequestDetail> Deduction { get; set; }
        public Models.Payroll Payroll { get; set; }
        public IEnumerable<Models.Employee> Employee { get; set; }
    }
}