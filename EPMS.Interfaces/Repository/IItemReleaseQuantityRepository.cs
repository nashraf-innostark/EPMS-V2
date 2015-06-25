using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IItemReleaseQuantityRepository : IBaseRepository<ItemReleaseQuantity, long>
    {
        long GetItemReleasedQuantity(long itemVariationId, long warehousrId);
    }
}
