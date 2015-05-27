using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class RFIItemRepository:BaseRepository<RFIItem>,IRFIItemRepository
    {
         #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RFIItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RFIItem> DbSet
        {
            get { return db.RFIItem; }
        }

        #endregion

        public IEnumerable<RFIItem> GetRfiItemsByRfiId(long rfiId)
        {
            return DbSet.Where(x => x.RFIId == rfiId);
        }
    }
}
