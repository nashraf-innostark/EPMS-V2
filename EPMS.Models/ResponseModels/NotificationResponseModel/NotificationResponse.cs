using System;
using System.ComponentModel.DataAnnotations;
using EPMS.Models.Common;

namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    public class NotificationResponse
    {
        public long NotificationId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "RequiredField")]
        [StringLength(160, ErrorMessage = "Title cannot exceed 160 characters.")]
        public string TitleE { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "RequiredField")]
        [StringLength(160, ErrorMessage = "Title cannot exceed 160 characters.")]
        public string TitleA { get; set; }
        public int CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public long ItemId { get; set; }
        public int AlertBefore { get; set; }
        public int AlertDateType { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "RequiredField")]
        public string AlertDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "RequiredField")]
        public string AlertDateHijri { get; set; }
        public string UserId { get; set; }
        [Range(1, 10000000000000000000, ErrorMessage = "Please enter a valid number between 1 and 20.")]
        public string MobileNo { get; set; }
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public bool ReadStatus { get; set; }
        public bool IsEmailSent { get; set; }
        public bool IsSMSsent { get; set; }


        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }
        public bool SystemGenerated { get; set; }
        public bool? ForAdmin { get; set; }
        public int? ForRole { get; set; }
        public long EmployeeId { get; set; }
        public string SmsText { get; set; }
        public string EmailText { get; set; }
        public bool TextForAdmin { get; set; }
        public string NotificationCode { get; set; }
    }
}
