using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class PhysicalCountRepository : BaseRepository<PhysicalCount>, IPhysicalCountRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PhysicalCountRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PhysicalCount> DbSet
        {
            get { return db.PhysicalCount; }
        }

        #endregion
    }
}
