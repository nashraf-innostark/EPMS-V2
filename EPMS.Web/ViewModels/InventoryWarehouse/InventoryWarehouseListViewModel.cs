using System.Collections.Generic;

namespace EPMS.Web.ViewModels.InventoryWarehouse
{
    public class InventoryWarehouseListViewModel
    {
        public Models.Warehouse Warehouse { get; set; }
        public IEnumerable<Models.Warehouse> Warehouses { get; set; }
    }
}