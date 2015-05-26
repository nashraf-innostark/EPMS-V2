using System.Data.Entity;
using EPMS.Repository.BaseRepository;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ItemImageRepository : BaseRepository<ItemImage>, IItemImageRepository
    {
        public ItemImageRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<ItemImage> DbSet
        {
            get { return db.ItemImages; }
        }
    }
}
