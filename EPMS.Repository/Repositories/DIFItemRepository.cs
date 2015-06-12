using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class DIFItemRepository : BaseRepository<DIFItem>, IDIFItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DIFItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<DIFItem> DbSet
        {
            get { return db.DIFItem; }
        }

        #endregion
        
        public IEnumerable<DIFItem> GetDifItemsById(long rfiId)
        {
            return DbSet.Where(x => x.DIFId == rfiId);
        }
    }
}
