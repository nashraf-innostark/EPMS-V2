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
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public short? OrderStatus { get; set; }
        public long? QuotationId { get; set; }
        public string CustomerNameE{ get; set; }
        public string CustomerNameA{ get; set; }
        public string QuotationNumber { get; set; }
        public long InvoiceId { get; set; }
        public long InvoiceNumber { get; set; }
        public IList<Receipt> Receipts { get; set; }
    }
}