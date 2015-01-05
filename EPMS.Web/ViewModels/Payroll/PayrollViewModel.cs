using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Payroll
{
    public class PayrollViewModel
    {
        public PayrollViewModel()
        {
            Employee = new Models.Employee();
            Allowances = new Allowance();
        }
        //public IEnumerable<Models.Payroll> Requests { get; set; }
        public Models.Employee Employee { get; set; }
        public Models.Allowance Allowances { get; set; }

        public double Deduction1 { get; set; }
        public double Deduction2 { get; set; }
        public double Total { get; set; }
    }
}