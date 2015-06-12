using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class DIFItemHistoryRepository : BaseRepository<DIFItemHistory>, IDIFItemHistoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DIFItemHistoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<DIFItemHistory> DbSet
        {
            get { return db.DifItemHistories; }
        }

        #endregion
    }
}
