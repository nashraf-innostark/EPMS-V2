using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class WarehouseDetailRepository : BaseRepository<WarehouseDetail>, IWarehouseDetailRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WarehouseDetailRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<WarehouseDetail> DbSet
        {
            get { return db.WarehouseDetails; }
        }

        #endregion

        #region Public
        #endregion
    }
}
