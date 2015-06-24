using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class ItemReleaseHistory
    {
        public long ItemReleaseId { get; set; }
        public string FormNumber { get; set; }
        public string CreatedBy { get; set; }
        public string ShipmentDetails { get; set; }
        public short? Status { get; set; }
        public long? RequesterId { get; set; }
        public long? RFIId { get; set; }
        public string OrderNo { get; set; }
        public string DeliveryInfo { get; set; }
        public string DeliveryInfoArabic { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
        public long QuantityReleased { get; set; }
        public string Notes { get; set; }
        public string NotesAr { get; set; }
        public string ManagerId { get; set; }
        public long ParentId { get; set; }

        public virtual AspNetUser Manager { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<ItemReleaseDetailHistory> ItemReleaseDetailHistories { get; set; }
        public virtual RFI RFI { get; set; }
    }
}
