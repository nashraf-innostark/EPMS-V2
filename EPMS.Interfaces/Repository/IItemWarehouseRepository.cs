using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IItemWarehouseRepository : IBaseRepository<ItemWarehouse, long>
    {
        IEnumerable<ItemWarehouse> GetItemsByVariationId(long variationId);
        long GetItemQuantity(long itemVariationId, long warehousrId);
        ItemWarehouse FindItemWarehouseByVariationAndManufacturerId(long variationId, long warehouseId);
        IEnumerable<ItemWarehouse> GetItemWarehousesByVariationId(long variationId);
    }
}
