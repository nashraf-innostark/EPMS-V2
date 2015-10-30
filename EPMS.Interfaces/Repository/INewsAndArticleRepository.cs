using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface INewsAndArticleRepository : IBaseRepository<NewsAndArticle, long>
    {
        NewsAndArticleResponse GetNewsAndArticleList(NewsAndArticleSearchRequest request, bool type);
        NewsAndArticleResponse GetNewsAndArticleListForSearch(NewsAndArticleSearchRequest request, string search);
        IEnumerable<NewsAndArticle> SearchInNewsAndArticle(NewsAndArticleSearchRequest request, string search);
    }
}
