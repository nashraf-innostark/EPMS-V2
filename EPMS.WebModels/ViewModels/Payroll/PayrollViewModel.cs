namespace EPMS.WebModels.ViewModels.Payroll
{
    public class PayrollViewModel
    {
        public PayrollViewModel()
        {
            Employee = new WebsiteModels.Employee();
            Allowances = new WebsiteModels.Allowance();
        }
        //public IEnumerable<Models.Payroll> Requests { get; set; }
        public WebsiteModels.Employee Employee { get; set; }
        public WebsiteModels.Allowance Allowances { get; set; }

        public double Deduction1 { get; set; }
        public double Deduction2 { get; set; }
        public double Total { get; set; }
        public string Date { get; set; }
        public long Id { get; set; }
    }
}