using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ItemReleaseQuantityRepository : BaseRepository<ItemReleaseQuantity>, IItemReleaseQuantityRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemReleaseQuantityRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemReleaseQuantity> DbSet
        {
            get { return db.ItemReleaseQuantities; }
        }

        #endregion
    }
}
