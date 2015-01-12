using System.Collections.Generic;

namespace EPMS.Web.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public IEnumerable<EmployeeRequestsViewModel> EmployeeRequests { get; set; }
    }
}