using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public interface IItemVariationService
    {
        IEnumerable<ItemVariation> GetAll();
        ItemVariation FindVariationById(long id);
        bool AddVariation(ItemVariation itemVariation);
        bool UpdateVariation(ItemVariation itemVariation);
        void DeleteVartiation(ItemVariation itemVariation);

    }
}
