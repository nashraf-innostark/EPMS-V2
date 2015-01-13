namespace EPMS.Web.DashboardModels
{
    public class EmployeeRequest
    {
        public long EmployeeId { get; set; }
        public long RequestId { get; set; }
        public string RequestTopic { get; set; }
        public string EmployeeNameE { get; set; }
        public string RequestDateString { get; set; }
        public bool IsReplied { get; set; }
    }
}