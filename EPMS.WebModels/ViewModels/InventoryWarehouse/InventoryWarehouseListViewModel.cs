using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.InventoryWarehouse
{
    public class InventoryWarehouseListViewModel
    {
        public WebsiteModels.Warehouse Warehouse { get; set; }
        public IEnumerable<WebsiteModels.Warehouse> Warehouses { get; set; }
    }
}