using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class RFQRepository : BaseRepository<RFQ>, IRFQRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RFQRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RFQ> DbSet
        {
            get { return db.Rfqs; }
        }

        #endregion
    }
}
