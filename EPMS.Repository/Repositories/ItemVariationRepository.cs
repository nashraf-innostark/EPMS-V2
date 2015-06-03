using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ItemVariationRepository : BaseRepository<ItemVariation>, IItemVariationRepository
    {
        public ItemVariationRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<ItemVariation> DbSet
        {
            get { return db.ItemVariations; }
        }

        public IEnumerable<ItemVariationDropDownListItem> GetItemVariationDropDownList()
        {
            return DbSet.Select(x => new ItemVariationDropDownListItem
            {
                ItemVariationId = x.ItemVariationId,
                ItemCodeSKUCode = x.InventoryItem.ItemCode + " - " + x.SKUCode,
                SKUCode = x.SKUCode,
                ItemVariationDescriptionA = x.DescriptionAr,
                ItemVariationDescriptionE = x.DescriptionEn,
                ItemNameA = x.InventoryItem.ItemNameAr,
                ItemNameE = x.InventoryItem.ItemNameEn
            });
        }
    }
}
