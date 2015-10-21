using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public IEnumerable<NewsAndArticle> SearchInNewsAndArticle(string search)
        {
            return
                DbSet.Where(
                    x =>
                        (x.TitleEn.Contains(search) || x.TitleAr.Contains(search) || 
                         x.AuthorNameEn.Contains(search) || x.AuthorNameAr.Contains(search) || 
                         x.ContentEn.Contains(search) || x.ContentAr.Contains(search) ||
                         x.MetaDesc.Contains(search) || x.MetaDescAr.Contains(search) ||
                         x.MetaKeywords.Contains(search) || x.MetaKeywordsAr.Contains(search)));
        }
    }
}
