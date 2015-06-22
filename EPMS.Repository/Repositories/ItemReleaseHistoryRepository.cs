using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ItemReleaseHistoryRepository : BaseRepository<ItemReleaseHistory>, IItemReleaseHistoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemReleaseHistoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemReleaseHistory> DbSet
        {
            get { return db.ReleaseHistories; }
        }

        #endregion
        public IEnumerable<ItemReleaseHistory> GetIrfHistoryData(long parentId)
        {
            return DbSet.Where(x => (x.Status == 1 || x.Status == 2) && x.ParentId == parentId).ToList();
        }
    }
}
