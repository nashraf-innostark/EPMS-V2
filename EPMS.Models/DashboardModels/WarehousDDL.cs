namespace EPMS.Models.DashboardModels
{
    public class WarehousDDL
    {
        public long WarehouseId { get; set; }
        public string WarehouseNumber { get; set; }
    }

    public class InventoryItemDDL
    {
        public long ItemId { get; set; }
        public string ItemNameE { get; set; }
        public string ItemNameA { get; set; }
    }
}