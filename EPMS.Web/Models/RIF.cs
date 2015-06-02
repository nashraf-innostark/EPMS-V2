using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class RIF
    {
        public long RIFId { get; set; }
        [Required]
        [Display(Name = "Client")]
        public long CustomerId { get; set; }
        [Required]
        [Display(Name = "Order")]
        public long OrderId { get; set; }
        [Required]
        [Display(Name = "Returning Reason")]
        public string ReasonE { get; set; }
        public string ReasonA { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        
        public int Status { get; set; }
        public string CustomerName { get; set; }
        public string RequesterName { get; set; }
        public string ManagerName { get; set; }
        public string OrderNo { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDateString { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }
        public string ManagerId { get; set; }
    }
}