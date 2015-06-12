using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class TIRItemRepository : BaseRepository<TIRItem>, ITIRItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TIRItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TIRItem> DbSet
        {
            get { return db.TIRItem; }
        }

        #endregion

        public IEnumerable<TIRItem> GetTirItemsById(long id)
        {
            return DbSet.Where(x => x.TIRId == id).ToList();
        }
    }
}
