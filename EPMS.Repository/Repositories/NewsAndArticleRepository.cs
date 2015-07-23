using System.Data.Entity;
using EPMS.Models.DomainModels;
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
    }
}
