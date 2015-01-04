using System.Collections.Generic;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Payroll
{
    public class PayrollViewModel
    {
        public PayrollViewModel()
        {
            Payroll = new Models.Payroll();
            Employee = new Models.Employee();
            Allowances = new Allowance();
        }
        public Models.Payroll Payroll { get; set; }
        public Models.Employee Employee { get; set; }
        public Models.Allowance Allowances { get; set; }
        public double Total { get; set; }
        public double Deduction1 { get; set; }
        public double Deduction2 { get; set; }
    }
}