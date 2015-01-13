using System.Collections.Generic;
using EPMS.Web.DashboardModels;

namespace EPMS.Web.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public long employeeId { get; set; }
        public long customerId { get; set; }
        public long complaintId { get; set; }
        public IEnumerable<EmployeeRequest> EmployeeRequests { get; set; }
        public IEnumerable<DashboardModels.Employee> Employees { get; set; }
        public IEnumerable<DashboardModels.Customer> Customers { get; set; }
        public IEnumerable<DashboardModels.Complaint> Complaints { get; set; }
    }
}