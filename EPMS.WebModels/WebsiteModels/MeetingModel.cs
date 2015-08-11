namespace EPMS.WebModels.WebsiteModels
{
    public class MeetingModel
    {
        public long MeetingId { get; set; }
        public string TopicName { get; set; }
        public string TopicNameAr { get; set; }
        public string RelatedProject { get; set; }
        public string Date { get; set; }
        public string DateString { get; set; }
    }
}