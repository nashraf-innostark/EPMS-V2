using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.Models.DomainModels
{
    public interface IItemVariationService
    {
        IEnumerable<ItemVariation> GetAll();
        ItemVariation FindVariationById(long id);
        bool AddVariation(ItemVariation itemVariation);
        bool UpdateVariation(ItemVariation itemVariation);
        void DeleteVartiation(ItemVariation itemVariation);
        IEnumerable<ItemVariationDropDownListItem> GetItemVariationDropDownList();
    }
}
