using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Repository.BaseRepository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ItemReleaseDetailRepository : BaseRepository<ItemReleaseDetail>, IItemReleaseDetailRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemReleaseDetailRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemReleaseDetail> DbSet
        {
            get { return db.ItemReleaseDetails; }
        }

        #endregion
    }
}
