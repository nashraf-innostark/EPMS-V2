using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class TIRHistoryRepository : BaseRepository<TIRHistory>, ITIRHistoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TIRHistoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TIRHistory> DbSet
        {
            get { return db.TirHistories; }
        }

        #endregion

        public IEnumerable<TIRHistory> GetTirHistoryData(long parentId)
        {
            return DbSet.Where(x => (x.Status == 1 || x.Status == 2) && x.ParentId == parentId).ToList();
        }
    }
}
