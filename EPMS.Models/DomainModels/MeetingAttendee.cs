namespace EPMS.Models.DomainModels
{
    public class MeetingAttendee
    {
        public long AttendeeId { get; set; }
        public long MeetingId { get; set; }
        public long EmployeeId { get; set; }
        public bool Status { get; set; }
        public long? NotificationId { get; set; }
        public bool? NotificationStatus { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Meeting Meeting { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
