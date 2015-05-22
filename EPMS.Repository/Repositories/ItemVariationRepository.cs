using System.Data.Entity;
using EPMS.Interfaces.Repository;
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
    }
}
