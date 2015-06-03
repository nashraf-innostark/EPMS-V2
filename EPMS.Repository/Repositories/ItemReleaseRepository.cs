using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ItemReleaseRepository : BaseRepository<ItemRelease>, IItemReleaseRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemReleaseRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemRelease> DbSet
        {
            get { return db.ItemReleases; }
        }

        #endregion
    }
}
