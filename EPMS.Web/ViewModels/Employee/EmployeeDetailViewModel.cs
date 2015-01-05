using EPMS.Web.ViewModels.Request;

namespace EPMS.Web.ViewModels.Employee
{
    public class EmployeeDetailViewModel
    {
        public EmployeeDetailViewModel()
        {
            EmployeeViewModel = new EmployeeViewModel();
            RequestViewModel = new RequestViewModel();
        }
        public EmployeeViewModel EmployeeViewModel { get; set; }

        public RequestViewModel RequestViewModel { get; set; }
        public string Role { get; set; }
    }
}