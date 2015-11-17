namespace EPMS.Models.RequestModels.Reports
{
    public class ProjectReportCreateOrDetailsRequest
    {
        public long ReportId { get; set; }
        public long ProjectId { get; set; }
        public string RequesterRole { get; set; }
        public string RequesterId { get; set; }
        public bool IsCreate { get; set; }
    }
    public class WarehouseReportCreateOrDetailsRequest
    {
        public long ReportId { get; set; }
        public long WarehouseId { get; set; }
        public string RequesterRole { get; set; }
        public string RequesterId { get; set; }
        public bool IsCreate { get; set; }
    }
}
