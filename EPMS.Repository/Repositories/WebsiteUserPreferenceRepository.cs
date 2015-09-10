using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class WebsiteUserPreferenceRepository : BaseRepository<WebsiteUserPrefrence>, IWebsiteUserPreferenceRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WebsiteUserPreferenceRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<WebsiteUserPrefrence> DbSet
        {
            get { return db.WebsiteUserPrefrences; }
        }

        #endregion

        #region Public

        public WebsiteUserPrefrence GetPrefrencesByUserId(string userId)
        {
            return DbSet.FirstOrDefault(x => x.UserId == userId);
        }

        #endregion
    }
}
