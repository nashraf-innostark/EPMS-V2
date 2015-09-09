using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class WebsiteHomePageRepository : BaseRepository<WebsiteHomePage>, IWebsiteHomePageRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WebsiteHomePageRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<WebsiteHomePage> DbSet
        {
            get { return db.WebsiteHomePages; }
        }

        #endregion

        #region Public
        public WebsiteHomePage GetHomePageLogo()
        {
            return DbSet.FirstOrDefault();
        }
        #endregion
    }
}
