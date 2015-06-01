using System.Collections.Generic;
using EPMS.Models.ResponseModels.EmployeeResponseModel;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.InventoryWarehouse
{
    public class InventoryWarehouseCreateViewModel
    {
        public InventoryWarehouseCreateViewModel()
        {
            Warehouse = new Warehouse
            {
                WarehouseDetails = new List<WarehouseDetail>()
            };
        }
        public Warehouse Warehouse { get; set; }
        public IEnumerable<Warehouse> Warehouses { get; set; }
        public IEnumerable<EmployeeDDL> Employees { get; set; }
    }
}