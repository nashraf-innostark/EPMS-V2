using System;

namespace EPMS.Models.DomainModels
{
    public class Complaint
    {
        public long ComplaintId { get; set; }
        public long CustomerId { get; set; }
        public long DepartmentId { get; set; }
        public long OrderId { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string Reply { get; set; }
        public bool IsReplied { get; set; }
        public int Status { get; set; }
        public DateTime ComplaintDate { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Department Department { get; set; }
        public virtual Order Order { get; set; }
    }
}
