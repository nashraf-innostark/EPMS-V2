using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IItemWarehouseService
    {
        IEnumerable<ItemWarehouse> GetAll();
        ItemWarehouse FindItemWarehouseById(long id);
        bool AddItemWarehouse(ItemWarehouse iteWarehouse);
        bool UpdateItemWarehouse(ItemWarehouse itemWarehouse);
        void DeleteItemWarehouse(ItemWarehouse itemWarehouse);
        IEnumerable<ItemWarehouse> GetItemsByVariationId(long variationId);
    }
}
