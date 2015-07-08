using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IItemVariationRepository : IBaseRepository<ItemVariation, long>
    {
        long GetItemVariationId(string item);
        IEnumerable<ItemVariationDropDownListItem> GetItemVariationDropDownList();
        IEnumerable<ItemVariation> GetVariationsByInventoryItemId(long inventoryItemId);
        ItemVariation FindVariationByBarcode(string barcode);
    }
}
