namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    public class NotificationRecipientResponse
    {
        public long Id { get; set; }
        public long NotificationId { get; set; }
        public bool IsRead { get; set; }
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public long? EmployeeId { get; set; }
    }
}
