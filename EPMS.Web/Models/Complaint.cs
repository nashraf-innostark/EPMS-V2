using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class Complaint
    {
        public long ComplaintId { get; set; }
        public long CustomerId { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Range(0, 9223372036854775807)]
        public long DepartmentId { get; set; }

        [Required(ErrorMessage = "Order is required.")]
        [Range(0, 9223372036854775807)]
        public long OrderId { get; set; }
        [Required(ErrorMessage = "Topic is required.")]
        public string Topic { get; set; }
        public string Description { get; set; }
        public string ComplaintDesc { get; set; }
        public string Reply { get; set; }
        public bool IsReplied { get; set; }
        public string IsRepliedString { get; set; }
        public int Status { get; set; }//1=resolved,2=in progress
        public DateTime ComplaintDate { get; set; }
        public string ComplaintDateString { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public string ClientName { get; set; }
    }
}