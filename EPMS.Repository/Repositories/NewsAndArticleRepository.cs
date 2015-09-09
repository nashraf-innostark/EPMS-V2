using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Web.UI.WebControls;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using EPMS.Interfaces.Repository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class NewsAndArticleRepository : BaseRepository<NewsAndArticle>, INewsAndArticleRepository
    {
        public NewsAndArticleRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<NewsAndArticle> DbSet
        {
            get { return db.NewsAndArticles; }
        }

        public NewsAndArticleResponse GetNewsAndArticleList(NewsAndArticleSearchRequest request, bool type)
        {
            int fromRow = request.iDisplayStart;
            int toRow = request.iDisplayLength;

            NewsAndArticleResponse response = new NewsAndArticleResponse
            {
                TotalCount = DbSet.Count(x => x.Type == type),
                NewsAndArticles = DbSet.Where(x => x.Type == type).OrderBy(x=>x.NewsArticleId).Skip(fromRow).Take(toRow).ToList()
            };

            return response;
        }
    }
}
