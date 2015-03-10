namespace EPMS.Models.DomainModels
{
    public class JobHistory
    {
        public string JobTitle { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double BasicSalary { get; set; }
        public double SalaryWithAllowances { get; set; }
        public double TotalSalaryReceived { get; set; }
    }
}
