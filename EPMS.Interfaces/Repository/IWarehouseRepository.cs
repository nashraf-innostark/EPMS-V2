using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface  IWarehouseRepository : IBaseRepository<Warehouse, long>
    {
        bool WarehouseExists(Warehouse warehouse);
    }
}
