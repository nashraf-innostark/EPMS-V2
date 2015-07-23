using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class PartnerRepository : BaseRepository<Partner>, IPartnerRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PartnerRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Partner> DbSet
        {
            get { return db.Partners; }
        }

        #endregion

        #region Public

        #endregion
    }
}
