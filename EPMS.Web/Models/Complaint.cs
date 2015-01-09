using System;

namespace EPMS.Web.Models
{
    public class Complaint
    {
        public long ComplaintId { get; set; }
        public long CustomerId { get; set; }
        public long DepartmentId { get; set; }
        public long OrderId { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string ComplaintDesc { get; set; }
        public string Reply { get; set; }
        public bool IsReplied { get; set; }
        public int Status { get; set; }//1=resolved,2=in progress
        public DateTime ComplaintDate { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public string ClientName { get; set; }
    }
}