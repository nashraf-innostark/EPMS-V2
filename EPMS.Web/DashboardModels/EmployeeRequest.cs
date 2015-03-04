namespace EPMS.Web.DashboardModels
{
    public class EmployeeRequest
    {
        public long EmployeeId { get; set; }
        public long RequestId { get; set; }
        public string RequestTopic { get; set; }
        public string EmployeeNameE { get; set; }
        public string EmployeeNameA { get; set; }
        public string RequestTopicShort { get; set; }
        public string EmployeeNameEShort { get; set; }
        public string EmployeeNameAShort { get; set; }
        public string RequestDateString { get; set; }
        public bool IsReplied { get; set; }
    }
}