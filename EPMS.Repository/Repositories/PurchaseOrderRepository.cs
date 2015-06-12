using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class PurchaseOrderRepository : BaseRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PurchaseOrderRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PurchaseOrder> DbSet
        {
            get { return db.PurchaseOrders; }
        }

        #endregion

        public PurchaseOrderListResponse GetAllPoS(PurchaseOrderSearchRequest searchRequest)
        {
            return null;
        }
    }
}
