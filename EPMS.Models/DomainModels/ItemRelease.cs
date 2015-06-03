using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class ItemRelease
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

        public virtual Customer Customer { get; set; }
        public virtual RFI RFI { get; set; }
        public virtual ICollection<ItemReleaseDetail> ItemReleaseDetails { get; set; }
    }
}
