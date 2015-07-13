using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class PhysicalCountItemRepository : BaseRepository<PhysicalCountItem>, IPhysicalCountItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PhysicalCountItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PhysicalCountItem> DbSet
        {
            get { return db.PhysicalCountItems; }
        }

        #endregion

        #region Public
        
        public IEnumerable<PhysicalCountItem> GetPCiItemsByPCiId(long pcId)
        {
            return DbSet.Where(x => x.PCId == pcId);
        }
        #endregion
    }
}
