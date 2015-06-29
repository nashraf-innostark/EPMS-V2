using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.MenuModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.DashboardModels;
using EPMS.Web.Models;
using DashboardWidgetPreference = EPMS.Web.Models.DashboardWidgetPreference;
using EmployeeRequest = EPMS.Web.DashboardModels.EmployeeRequest;
using Order = EPMS.Web.DashboardModels.Order;

namespace EPMS.Web.ViewModels.Dashboard
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
        public IEnumerable<DashboardModels.Department> Departments { get; set; }
        public IEnumerable<DashboardModels.Employee> Employees { get; set; }
        
        public IEnumerable<DashboardModels.Employee> EmployeesRecent { get; set; }
        public IEnumerable<DashboardModels.Customer> Customers { get; set; }
        public IEnumerable<DashboardModels.Complaint> Complaints { get; set; }

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
        public IEnumerable<DashboardModels.Recruitment> Recruitments { get; set; }
        public Profile Profile { get; set; }
        public IEnumerable<DashboardModels.Meeting> Meetings { get; set; }
        public DashboardModels.Payroll Payroll { get; set; }
        public ProjectResponseForDashboard Project { get; set; }
        public IEnumerable<DashboardModels.Project> ProjectsDDL { get; set; }
        public IEnumerable<DashboardModels.Project> TaskProjectsDDL { get; set; }
        public IEnumerable<ProjectTaskResponse> ProjectTasks { get; set; }
        public IEnumerable<DashboardWidgetPreference> WidgetPreferenceses { get; set; }
        public IEnumerable<QuickLaunchMenuItems> QuickLaunchItems { get; set; }
        public IEnumerable<QuickLaunchItem> QuickLaunchUserItems { get; set; }
        public IEnumerable<QuickLaunchUserItems> LaunchItems { get; set; }
    }
}