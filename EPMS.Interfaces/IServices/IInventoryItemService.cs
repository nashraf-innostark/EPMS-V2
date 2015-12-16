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
        string DeleteItem(long itemId);
        SaveInventoryItemResponse SaveItem(InventoryItemRequest itemToSave);
        InventoryItemResponse GetAllInventoryItems(InventoryItemSearchRequest searchRequest);
    }
}
