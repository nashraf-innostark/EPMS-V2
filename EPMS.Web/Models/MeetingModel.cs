using System;

namespace EPMS.Web.Models
{
    public class MeetingModel
    {
        public long MeetingId { get; set; }
        public string TopicName { get; set; }
        public string TopicNameAr { get; set; }
        public string RelatedProject { get; set; }
        public DateTime? Date { get; set; }
        public string DateString { get; set; }
    }
}