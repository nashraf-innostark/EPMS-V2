using System;

namespace EPMS.Web.Models
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
        public string CustomerNameE{ get; set; }
        public string CustomerNameA{ get; set; }
    }
}