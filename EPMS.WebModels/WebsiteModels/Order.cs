using System;
using System.Collections.Generic;

namespace EPMS.WebModels.WebsiteModels
{
    public class Order
    {
        public long OrderId { get; set; }
        public string OrderNo { get; set; }
        public string OrderDescription { get; set; }
        public string OrderNotes { get; set; }
        public long CustomerId { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDate { get; set; }
        public string RecCreatedDateStr { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public short? OrderStatus { get; set; }
        public long? QuotationId { get; set; }
        public string CustomerNameE{ get; set; }
        public string CustomerNameA{ get; set; }
        public string QuotationNumber { get; set; }
        public long InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public bool FromOrder{ get; set; }
        public IList<Receipt> Receipts { get; set; }
    }
}