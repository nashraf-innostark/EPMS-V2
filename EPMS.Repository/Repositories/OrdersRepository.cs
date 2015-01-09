using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;
using EPMS.Repository.BaseRepository;

namespace EPMS.Repository.Repositories
{
    class OrdersRepository : BaseRepository<Orders>, IOrdersRepository
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
        protected override IDbSet<Orders> DbSet
        {
            get { return db.Orders; }
        }

        #endregion

        #region Public
        #endregion
    }
}
