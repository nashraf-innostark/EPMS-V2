namespace EPMS.Web.DashboardModels
{
    public class Complaint
    {
        public long ComplaintId { get; set; }
        public string Topic { get; set; }
        public string ClientName { get; set; }
        public string TopicShort { get; set; }
        public string ClientNameShort { get; set; }
        public bool IsReplied { get; set; }
    }
}