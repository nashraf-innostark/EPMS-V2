using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class RFQ
    {
        public long RFQId { get; set; }
        public string Notes { get; set; }
        public short? Discount { get; set; }
        public string Requests { get; set; }
        public long? CustomerId { get; set; }
        public short? Status { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<RFQItem> RFQItems { get; set; }
    }
}
