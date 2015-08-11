using System.Collections.Generic;
using EPMS.Models.DashboardModels;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;
using EmployeeRequest = EPMS.Models.DashboardModels.EmployeeRequest;
using Order = EPMS.Models.DashboardModels.Order;

namespace EPMS.WebModels.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public string UserMac { get; set; }
        public string LicenseMac { get; set; }
        public long employeeId { get; set; }
        public long customerId { get; set; }
        public long departmentId { get; set; }
        public long projectId { get; set; }
        public long customerIdForOrder { get; set; }
        public long complaintId { get; set; }
        public IEnumerable<EmployeeRequest> EmployeeRequests { get; set; }
        public IEnumerable<Models.DashboardModels.Department> Departments { get; set; }
        public IEnumerable<Models.DashboardModels.Employee> Employees { get; set; }
        
        public IEnumerable<Models.DashboardModels.Employee> EmployeesRecent { get; set; }
        public IEnumerable<Models.DashboardModels.Customer> Customers { get; set; }
        public IEnumerable<Models.DashboardModels.Complaint> Complaints { get; set; }

        #region Inventory Widgets Properties
        public IEnumerable<WarehousDDL> Warehouses { get; set; }
        public IEnumerable<RFIWidget> RFI { get; set; }
        public IEnumerable<IRFWidget> IRF { get; set; }
        public IEnumerable<RIFWidget> RIF { get; set; }
        public IEnumerable<DIFWidget> DIF { get; set; }
        public IEnumerable<TIRWidget> TIR { get; set; }
        public IEnumerable<POWidget> PO { get; set; }
        #endregion

        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Models.DashboardModels.Recruitment> Recruitments { get; set; }
        public Profile Profile { get; set; }
        public IEnumerable<Models.DashboardModels.Meeting> Meetings { get; set; }
        public Models.DashboardModels.Payroll Payroll { get; set; }
        public ProjectResponseForDashboard Project { get; set; }
        public IEnumerable<Models.DashboardModels.Project> ProjectsDDL { get; set; }
        public IEnumerable<Models.DashboardModels.Project> TaskProjectsDDL { get; set; }
        public IEnumerable<ProjectTaskResponse> ProjectTasks { get; set; }
        public IEnumerable<WebsiteModels.DashboardWidgetPreference> WidgetPreferenceses { get; set; }
        public IEnumerable<WebsiteModels.QuickLaunchMenuItems> QuickLaunchItems { get; set; }
        public IEnumerable<QuickLaunchItem> QuickLaunchUserItems { get; set; }
        public IEnumerable<WebsiteModels.QuickLaunchUserItems> LaunchItems { get; set; }
    }
}