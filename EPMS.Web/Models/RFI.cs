using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class RFI
    {
        public long RFIId { get; set; }
        [Required]
        [Display(Name = "Client")]
        public long CustomerId { get; set; }
        [Required]
        [Display(Name = "Order")]
        public long OrderId { get; set; }
        [Required]
        [Display(Name = "Usage")]
        public string OrderNumber { get; set; }
        public string UsageE { get; set; }
        public string UsageA { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        
        public int Status { get; set; }
        public string CustomerDeliveryInfo { get; set; }
        public string CustomerName { get; set; }
        public string RequesterName { get; set; }
        public string RequesterNameAr { get; set; }
        public string ManagerName { get; set; }
        public string ManagerNameAr { get; set; }
        public string ManagerId { get; set; }

        public string OrderNo { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDateString { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }
    }
}