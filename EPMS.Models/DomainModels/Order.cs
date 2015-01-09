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

        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}
