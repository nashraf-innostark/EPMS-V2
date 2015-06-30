using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IItemManufacturerRepository : IBaseRepository<ItemManufacturer, long>
    {
        IEnumerable<ItemManufacturer> GetItemsByVariationId(long variationId);
        ItemManufacturer FindItemManufacturerByVariationAndManufacturerId(long variationId, long manufacturerId);
    }
}
