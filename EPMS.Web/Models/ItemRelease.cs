using System;

namespace EPMS.Web.Models
{
    public class ItemRelease
    {
        public long ItemReleaseId { get; set; }
        public string EmpJobId { get; set; }
        public string FormNumber { get; set; }
        public string CreatedBy { get; set; }
        public string ShipmentDetails { get; set; }
        public short? Status { get; set; }
        public string RequesterId { get; set; }
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
        public string RequesterNameE { get; set; }
        public string RequesterNameA { get; set; }
        public string ManagerName { get; set; }
        public string ManagerNameAr { get; set; }
        public string RequesterName { get; set; }
        public string RequesterNameAr { get; set; }
    }
}