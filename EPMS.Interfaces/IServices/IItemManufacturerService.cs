using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IItemManufacturerService
    {
        IEnumerable<ItemManufacturer> GetAll();
        ItemManufacturer FindItemManufacturerById(long id);
        bool AddItemManufacturer(ItemManufacturer itemanufacturer);
        bool UpdateItemManufacturer(ItemManufacturer itemManufacturer);
        void DeleteItemManufacturer(ItemManufacturer itemManufacturer);
        IEnumerable<ItemManufacturer> GetItemsByVariationId(long variationId);
    }
}
