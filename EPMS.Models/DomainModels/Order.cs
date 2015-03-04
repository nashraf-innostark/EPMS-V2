using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Order
    {
        public long OrderId { get; set; }
        public string OrderNo { get; set; }
        public string OrderDescription { get; set; }
        public string OrderNotes { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Attachment { get; set; }
        public long CustomerId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public int? OrderStatus { get; set; }

        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
