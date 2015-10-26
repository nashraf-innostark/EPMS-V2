using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
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

        public WebsiteSearchResponse SearchInWebsiteService(WebsiteServiceSearchRequest searchRequest, string search)
        {

            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayLength;

            WebsiteSearchResponse response = new WebsiteSearchResponse
            {

                TotalCount = DbSet.Count(x => (x.ServiceNameEn.Contains(search) || x.ServiceNameAr.Contains(search) ||
                                               x.DescriptionEn.Contains(search) || x.DescriptionAr.Contains(search) ||
                                               x.MetaDescriptionEn.Contains(search) || x.MetaDescriptionAr.Contains(search) ||
                                               x.MetaKeywordsEn.Contains(search) || x.MetaKeywordsAr.Contains(search)
                    )),

                WebsiteServices = DbSet.Where(x => (x.ServiceNameEn.Contains(search) || x.ServiceNameAr.Contains(search) ||
                x.DescriptionEn.Contains(search) || x.DescriptionAr.Contains(search) ||
                x.MetaDescriptionEn.Contains(search) || x.MetaDescriptionAr.Contains(search) ||
                x.MetaKeywordsEn.Contains(search) || x.MetaKeywordsAr.Contains(search)
                )).OrderBy(x => x.ServiceId).Skip(fromRow).Take(toRow).ToList()
            };
            return response;
        }
    }
}
