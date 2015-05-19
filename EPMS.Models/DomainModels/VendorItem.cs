namespace EPMS.Models.DomainModels
{
    public class VendorItem
    {
        public long ItemId { get; set; }
        public string ItemDetails { get; set; }
        public long VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
