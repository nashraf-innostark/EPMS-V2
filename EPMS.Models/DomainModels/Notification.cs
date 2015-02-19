using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Notification
    {
        public long NotificationId { get; set; }
        public string TitleE { get; set; }
        public string TitleA { get; set; }
        public int CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public int AlertBefore { get; set; }
        public int AlertDateType { get; set; }
        public DateTime AlertDate { get; set; }
        public bool SystemGenerated { get; set; }
        public DateTime AlertAppearDate { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }

        public virtual ICollection<MeetingAttendee> MeetingAttendees { get; set; }
        public virtual ICollection<NotificationRecipient> NotificationRecipients { get; set; }
    }
}
