using System;
using System.ComponentModel.DataAnnotations;

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
        public int AlertBefore { get; set; }
        public int AlertDateType { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "RequiredField")]
        public string AlertDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "RequiredField")]
        public string AlertDateHijri { get; set; }
        public long? EmployeeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "RequiredField")]
        [Range(1, 10000000000000000000, ErrorMessage = "Please enter a valid number between 1 and 20.")]
        public string MobileNo { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "RequiredField")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public bool ReadStatus { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }
        public bool SystemGenerated { get; set; }
    }
}
