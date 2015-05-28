using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface  IWarehouseService
    {
        IEnumerable<Warehouse> GetAll();
        Warehouse FindWarehouseById(long id);
        bool AddWarehouse(Warehouse warehouse);
        bool Updatewarehouse(Warehouse warehouse);
        void DeleteWarehouse(Warehouse warehouse);
    }
}
