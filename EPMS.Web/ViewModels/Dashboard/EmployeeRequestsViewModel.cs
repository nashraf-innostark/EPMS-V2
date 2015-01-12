namespace EPMS.Web.ViewModels.Dashboard
{
    public class EmployeeRequestsViewModel
    {
        public long EmployeeId { get; set; }
        public long RequestId { get; set; }
        public string RequestTopic { get; set; }
        public string EmployeeNameE { get; set; }
        public string RequestDateString { get; set; }
    }
}