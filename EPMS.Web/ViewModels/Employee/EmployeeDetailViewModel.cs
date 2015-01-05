using EPMS.Web.ViewModels.Request;

namespace EPMS.Web.ViewModels.Employee
{
    public class EmployeeDetailViewModel
    {
        public EmployeeDetailViewModel()
        {
            EmployeeViewModel = new EmployeeViewModel();
            EmployeeRequestViewModel = new RequestViewModel();
            RequestListViewModel = new RequestListViewModel();
        }
        public EmployeeViewModel EmployeeViewModel { get; set; }

        public RequestViewModel EmployeeRequestViewModel { get; set; }
        public RequestListViewModel RequestListViewModel { get; set; }
        public string Role { get; set; }
    }
}