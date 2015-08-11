using System.Collections.Generic;
using EPMS.WebModels.ViewModels.Request;

namespace EPMS.WebModels.ViewModels.Employee
{
    public class EmployeeDetailViewModel
    {
        public EmployeeDetailViewModel()
        {
            EmployeeViewModel = new EmployeeViewModel();
            EmployeeRequestViewModel = new RequestViewModel();
            RequestListViewModel = new RequestListViewModel();
            TaskEmployees = new List<WebsiteModels.TaskEmployee>();
        }
        public EmployeeViewModel EmployeeViewModel { get; set; }

        public RequestViewModel EmployeeRequestViewModel { get; set; }
        public RequestListViewModel RequestListViewModel { get; set; }
        public IList<WebsiteModels.TaskEmployee> TaskEmployees { get; set; }
        public string Role { get; set; }
    }
}