using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Repository.BaseRepository;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ItemManufacturerRepository : BaseRepository<ItemManufacturer>, IItemManufacturerRepository
    {
        public ItemManufacturerRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<ItemManufacturer> DbSet
        {
            get { return db.ItemManufacturers; }
        }

        public IEnumerable<ItemManufacturer> GetItemsByVariationId(long variationId)
        {
            return
                DbSet.Where(x => x.ItemVariationId == variationId);
        }

        public ItemManufacturer FindItemManufacturerByVariationAndManufacturerId(long variationId, long manufacturerId)
        {
            return
                DbSet.FirstOrDefault(x => x.ItemVariationId == variationId && x.ManufacturerId == manufacturerId);
        }
    }
}
