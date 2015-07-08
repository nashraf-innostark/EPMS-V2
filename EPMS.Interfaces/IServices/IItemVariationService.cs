using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IItemVariationService
    {
        IEnumerable<ItemVariation> GetAll();
        ItemVariation FindVariationById(long id);
        ItemVariation FindVariationByBarcode(string barcode);
        long[] GetItemVariationId(string[] items);
        bool AddVariation(ItemVariation itemVariation);
        bool UpdateVariation(ItemVariation itemVariation);
        void DeleteVartiation(ItemVariation itemVariation);
        ItemVariationResponse SaveItemVariation(ItemVariationRequest variationToSave);
        IEnumerable<ItemVariation> GetVariationsByInventoryItemId(long inventoryItemId);
        IEnumerable<ItemVariationDropDownListItem> GetItemVariationDropDownList();

        ItemVariationResponse ItemVariationResponse(long id, long itemVariationId);
    }
}
