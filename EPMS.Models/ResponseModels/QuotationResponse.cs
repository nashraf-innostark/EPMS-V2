using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class QuotationResponse
    {
        public IEnumerable<Quotation> Quotations { get; set; }
        public Quotation Quotation { get; set; }
        public RFQ Rfq { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
