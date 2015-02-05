namespace EPMS.Models.RequestModels.NotificationRequestModels
{
    public class NotificationRequestParams
    {
        public string RoleName { get; set; }
        public long EmployeeId { get; set; }
        public long CustomerId { get; set; }
        public string UserId { get; set; }
        public bool SystemGenerated { get; set; }
    }
}
