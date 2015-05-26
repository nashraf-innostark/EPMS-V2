using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IInventoryItemService
    {
        IEnumerable<InventoryItem> GetAll();
        InventoryItem FindItemById(long id);
        bool AddItem(InventoryItem item);
        bool UpdateItem(InventoryItem item);
        void DeleteItem(InventoryItem item);
        SaveInventoryItemResponse SaveItem(InventoryItemRequest itemToSave);
    }
}
