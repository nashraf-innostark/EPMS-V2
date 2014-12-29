using EPMS.Web.ViewModels.Request;

namespace EPMS.Web.ViewModels.Employee
{
    public class EmployeeDetailViewModel
    {
        public EmployeeDetailViewModel()
        {
            EmployeeViewModel = new EmployeeViewModel();
            EmployeeRequestViewModel = new EmployeeRequestViewModel();
        }
        public EmployeeViewModel EmployeeViewModel { get; set; }

        public EmployeeRequestViewModel EmployeeRequestViewModel { get; set; }
        public string Role { get; set; }
    }
}