using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;

namespace EPMS.Interfaces.IServices
{
    public interface IWarehouseService
    {
        IEnumerable<Warehouse> GetAll();
        Warehouse FindWarehouseById(long id);
        WarehouseRequest GetWarehouseRequest(long id);
        string GetLastWarehouseNumber();
        bool AddWarehouse(Warehouse warehouse);
        bool Updatewarehouse(Warehouse warehouse);
        void DeleteWarehouse(Warehouse warehouse);
    }
}
