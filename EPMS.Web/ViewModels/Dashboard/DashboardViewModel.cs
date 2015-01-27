using System.Collections.Generic;
using EPMS.Models.ResponseModels;
using EPMS.Web.DashboardModels;

namespace EPMS.Web.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public long employeeId { get; set; }
        public long customerId { get; set; }
        public long departmentId { get; set; }
        public long projectId { get; set; }
        public long customerIdForOrder { get; set; }
        public long complaintId { get; set; }
        public IEnumerable<EmployeeRequest> EmployeeRequests { get; set; }
        public IEnumerable<DashboardModels.Department> Departments { get; set; }
        public IEnumerable<DashboardModels.Employee> Employees { get; set; }
        public IEnumerable<DashboardModels.Employee> EmployeesRecent { get; set; }
        public IEnumerable<DashboardModels.Customer> Customers { get; set; }
        public IEnumerable<DashboardModels.Complaint> Complaints { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<DashboardModels.Recruitment> Recruitments { get; set; }
        public Profile Profile { get; set; }
        public IEnumerable<DashboardModels.Meeting> Meetings { get; set; }
        public DashboardModels.Payroll Payroll { get; set; }
        public ProjectResponseForDashboard Project { get; set; }
        public IEnumerable<DashboardModels.Project> ProjectsDDL { get; set; }
        public IEnumerable<DashboardModels.Project> TaskProjectsDDL { get; set; }
        public IEnumerable<ProjectTaskResponse> ProjectTasks { get; set; }
    }
}