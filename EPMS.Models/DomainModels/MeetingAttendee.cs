using System;

namespace EPMS.Models.DomainModels
{
    public class MeetingAttendee
    {
        public long AttendeeId { get; set; }
        public long MeetingId { get; set; }
        public long? EmployeeId { get; set; }
        public string AttendeeName { get; set; }
        public string AttendeeEmail { get; set; }
        public bool? Status { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual Meeting Meeting { get; set; }
    }
}
