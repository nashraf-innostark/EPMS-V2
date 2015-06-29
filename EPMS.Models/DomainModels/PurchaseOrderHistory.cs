using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class PurchaseOrderHistory
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
        public long ParentId { get; set; }

        public virtual AspNetUser Manager { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<PurchaseOrderItemHistory> PurchaseOrderItemHistories { get; set; }
    }
}
