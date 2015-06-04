using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.RequestModels
{
    public class WarehouseRequest
    {
        public Warehouse Warehouse { get; set; }
        public IEnumerable<Warehouse> Warehouses { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
