using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Request
{
    public class EmployeeRequestViewModel
    {
        public EmployeeRequest EmployeeRequest { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDepartment { get; set; }
    }
}