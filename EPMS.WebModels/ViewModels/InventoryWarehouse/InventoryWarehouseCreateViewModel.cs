using System.Collections.Generic;
using EPMS.Models.ResponseModels.EmployeeResponseModel;

namespace EPMS.WebModels.ViewModels.InventoryWarehouse
{
    public class InventoryWarehouseCreateViewModel
    {
        public InventoryWarehouseCreateViewModel()
        {
            Warehouse = new WebsiteModels.Warehouse
            {
                WarehouseDetails = new List<WebsiteModels.WarehouseDetail>()
            };
        }
        public WebsiteModels.Warehouse Warehouse { get; set; }
        public IEnumerable<WebsiteModels.Warehouse> Warehouses { get; set; }
        public IEnumerable<EmployeeDDL> Employees { get; set; }
    }
}