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
    public class PoItemRepository : BaseRepository<PurchaseOrderItem>, IPoItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PoItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PurchaseOrderItem> DbSet
        {
            get { return db.PurchaseOrderItems; }
        }

        #endregion

        public IEnumerable<PurchaseOrderItem> GetPoItemsByPoId(long id)
        {
            return DbSet.Where(x => x.PurchaseOrderId == id).ToList();
        }
    }
}
