using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class RFIHistoryRepository : BaseRepository<RFIHistory>, IRFIHistoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RFIHistoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RFIHistory> DbSet
        {
            get { return db.RfiHistories; }
        }

        #endregion
        public IEnumerable<RFIHistory> GetRfiHistoryData(long parentId)
        {
            return DbSet.Where(x => (x.Status == 3 || x.Status == 2) && x.ParentId == parentId).ToList();
        }
    }
}
