using System;

namespace EPMS.Web.Models
{
    public class PurchaseOrder
    {
        public long PurchaseOrderId { get; set; }
        public string FormNumber { get; set; }
        public string NotesA { get; set; }
        public string NotesE { get; set; }
        public int Status { get; set; }
        public string ManagerId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
    }
}