using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class LicenseControlPanelRepository : BaseRepository<LicenseControlPanel>, ILicenseControlPanelRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LicenseControlPanelRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<LicenseControlPanel> DbSet
        {
            get { return db.LicenseControlPanels; }
        }

        #endregion
    }
}
