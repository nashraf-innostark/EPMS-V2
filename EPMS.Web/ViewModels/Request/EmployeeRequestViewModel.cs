using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Request
{
    public class EmployeeRequestViewModel
    {
        public EmployeeRequestViewModel()
        {
            EmployeeRequest = new EmployeeRequest();
            EmployeeRequestDetail = new RequestDetail();

        }

        public EmployeeRequest EmployeeRequest { get; set; }
        public RequestDetail EmployeeRequestDetail { get; set; }
        public string RequestDescription { get; set; }
    }
}