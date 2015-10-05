using System;
using System.Collections.Generic;

namespace EPMS.WebModels.WebsiteModels
{
    public class RFQ
    {
        public RFQ()
        {
            RFQItems = new List<RFQItem>();
        }
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
        public string CustomerNameEn { get; set; }
        public string CustomerNameAr { get; set; }

        public IList<RFQItem> RFQItems { get; set; }
    }
}