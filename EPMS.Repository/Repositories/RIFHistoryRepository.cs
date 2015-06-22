using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class RIFHistoryRepository : BaseRepository<RIFHistory>, IRIFHistoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RIFHistoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RIFHistory> DbSet
        {
            get { return db.RifHistories; }
        }

        #endregion

        public IEnumerable<RIFHistory> GetRifHistoryData(long parentId)
        {
            return DbSet.Where(x => (x.Status == 3 || x.Status == 2) && x.ParentId == parentId).ToList();
        }
    }
}
