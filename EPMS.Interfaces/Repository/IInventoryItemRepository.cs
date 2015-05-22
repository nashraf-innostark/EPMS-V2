using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IInventoryItemRepository : IBaseRepository<InventoryItem, long>
    {
        bool ItemExists(InventoryItem item);
    }
}
