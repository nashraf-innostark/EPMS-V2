namespace EPMS.Models.RequestModels.Reports
{
    public class TaskReportCreateOrDetailsRequest
    {
        public long ReportId { get; set; }
        public long ProjectId { get; set; }
        public long TaskId { get; set; }
        public string RequesterId { get; set; }
        public bool IsCreate { get; set; }
    }
}
