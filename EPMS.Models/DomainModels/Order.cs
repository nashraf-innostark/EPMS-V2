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
        public long CustomerId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public short? OrderStatus { get; set; }
        public long? QuotationId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Quotation Quotation { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<RFI> RFIs { get; set; }
        public virtual ICollection<RFIHistory> RFIHistories { get; set; }
        public virtual ICollection<RIF> RIFs { get; set; }
        public virtual ICollection<RIFHistory> RIFHistories { get; set; }
    }
}
