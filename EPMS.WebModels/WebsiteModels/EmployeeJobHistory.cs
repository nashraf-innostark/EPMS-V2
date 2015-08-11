namespace EPMS.WebModels.WebsiteModels
{
    public class EmployeeJobHistory
    {
        public string JobTitle { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double BasicSalary { get; set; }
        public double SalaryWithAllowances { get; set; }
        public double TotalSalaryReceived { get; set; }
    }
}