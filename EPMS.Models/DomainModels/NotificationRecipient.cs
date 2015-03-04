namespace EPMS.Models.DomainModels
{
    public class NotificationRecipient
    {
        public long Id { get; set; }
        public long NotificationId { get; set; }
        public bool IsRead { get; set; }
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public long? EmployeeId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
