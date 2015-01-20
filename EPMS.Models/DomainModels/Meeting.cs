using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Meeting
    {
        public long MeetingId { get; set; }
        public string TopicName { get; set; }
        public string TopicNameAr { get; set; }
        public string RelatedProject { get; set; }
        public DateTime? Date { get; set; }
        public string Agenda { get; set; }
        public string AgendaAr { get; set; }
        public string Discussion { get; set; }
        public string DiscusionAr { get; set; }
        public string Decisions { get; set; }
        public string DecisionsAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual ICollection<MeetingAttendee> MeetingAttendees { get; set; }
        public virtual ICollection<MeetingDocument> MeetingDocuments { get; set; }
    }
}
