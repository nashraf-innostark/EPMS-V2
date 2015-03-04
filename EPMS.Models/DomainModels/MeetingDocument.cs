using System;

namespace EPMS.Models.DomainModels
{
    public class MeetingDocument
    {
        public long DocumentId { get; set; }
        public long MeetingId { get; set; }
        public string FileName { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }

        public virtual Meeting Meeting { get; set; }
    }
}
