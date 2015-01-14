using System;

namespace EPMS.Web.DashboardModels
{
    public class Profile
    {
        public long EmployeeId { get; set; }
        public string EmployeeNameE { get; set; }
        public string EmployeeNameA { get; set; }
        public string EmployeeImagePath { get; set; }
        public string EmployeeJobId { get; set; }
        public string EmployeeJobTitleE { get; set; }
        public string EmployeeJobTitleA { get; set; }
        public DateTime? EmployeeIqamaExpiryDt { get; set; }
    }
}