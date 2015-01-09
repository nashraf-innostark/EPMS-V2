using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class OrdersRepository : BaseRepository<Order>, IOrdersRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrdersRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Order> DbSet
        {
            get { return db.Orders; }
        }

        #endregion

        #region Public
        #endregion
    }
}
