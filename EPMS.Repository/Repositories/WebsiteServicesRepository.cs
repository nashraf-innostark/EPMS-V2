using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public IEnumerable<WebsiteService> SearchInWebsiteService(string search)
        {
            return DbSet.Where(x => (x.ServiceNameEn.Contains(search) || x.ServiceNameAr.Contains(search) ||
                x.DescriptionEn.Contains(search)||x.DescriptionAr.Contains(search)||
                x.MetaDescriptionEn.Contains(search)||x.MetaDescriptionAr.Contains(search)||
                x.MetaKeywordsEn.Contains(search)||x.MetaKeywordsAr.Contains(search)
                ));
        }
    }
}
