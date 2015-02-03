namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    public class NotificationListResponse
    {
        public long NotificationId { get; set; }
        public int SerialNo { get; set; }
        public string NotificationName { get; set; }
        public string CategoryName { get; set; }
        public string AlertTime { get; set; }
        public string AlertEndTime { get; set; }
        public string EmployeeName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Notified { get; set; }
        public long EmployeeId { get; set; }
    }
}
