using System.Collections.Generic;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Request;
using EPMS.Web.ViewModels.Tasks;

namespace EPMS.Web.ViewModels.Employee
{
    public class EmployeeDetailViewModel
    {
        public EmployeeDetailViewModel()
        {
            EmployeeViewModel = new EmployeeViewModel();
            EmployeeRequestViewModel = new RequestViewModel();
            RequestListViewModel = new RequestListViewModel();
            TaskEmployees = new List<TaskEmployee>();
        }
        public EmployeeViewModel EmployeeViewModel { get; set; }

        public RequestViewModel EmployeeRequestViewModel { get; set; }
        public RequestListViewModel RequestListViewModel { get; set; }
        public IList<TaskEmployee> TaskEmployees { get; set; }
        public string Role { get; set; }
    }
}