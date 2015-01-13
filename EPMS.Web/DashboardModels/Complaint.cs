namespace EPMS.Web.DashboardModels
{
    public class Complaint
    {
        public long ComplaintId { get; set; }
        public string Topic { get; set; }
        public string ClientName { get; set; }
        public bool IsReplied { get; set; }
    }
}