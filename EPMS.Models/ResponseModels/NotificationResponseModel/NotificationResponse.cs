using System;

namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    public class NotificationResponse
    {
        public long NotificationId { get; set; }
        public string TitleE { get; set; }
        public string TitleA { get; set; }
        public int CategoryId { get; set; }
        public int AlertBefore { get; set; }
        public int AlertDateType { get; set; }
        public string AlertDate { get; set; }
        public long? EmployeeId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool ReadStatus { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }
    }
}
