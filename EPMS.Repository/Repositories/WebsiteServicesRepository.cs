using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class WebsiteServicesRepository : BaseRepository<WebsiteService>, IWebsiteServicesRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WebsiteServicesRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<WebsiteService> DbSet
        {
            get { return db.WebsiteServices; }
        }

        #endregion
    }
}