namespace EPMS.Models.RequestModels.Reports
{
    public class ProjectReportCreateOrDetailsRequest
    {
        public long ProjectId { get; set; }
        public string Requester { get; set; }
        public bool IsCreate { get; set; }
    }
}
