namespace EPMS.Models.DomainModels
{
    public class ReportInventoryItem
    {
        public long ReportInventoryItemId { get; set; }
        public long ReportId { get; set; }
        public long InventoryItemId { get; set; }
        public string NameE { get; set; }
        public string NameA { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }
        public int SoldQty { get; set; }
        public int DefectiveQty { get; set; }
        public string BestPriceVendorNameE { get; set; }
        public string BestPriceVendorNameA { get; set; }
        public string MostBoughtVendorNameE { get; set; }
        public string MostBoughtVendorNameA { get; set; }

        public virtual Report Report { get; set; }
    }
}
