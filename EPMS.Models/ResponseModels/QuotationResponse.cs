using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class QuotationResponse
    {
        public bool Status { get; set; }
        public IEnumerable<Quotation> Quotations { get; set; }
        public IEnumerable<RFQ> Rfqs { get; set; }
        public Quotation Quotation { get; set; }
        public RFQ Rfq { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        // items for inventory pop up
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
