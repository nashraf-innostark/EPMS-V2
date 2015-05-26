using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class RFIRepository : BaseRepository<RFI>, IRFIRepository
    {
         #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RFIRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RFI> DbSet
        {
            get { return db.RFI; }
        }

        #endregion
    }
}
