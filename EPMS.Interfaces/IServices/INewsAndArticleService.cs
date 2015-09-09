using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface INewsAndArticleService
    {
        IEnumerable<NewsAndArticle> GetAll();
        NewsAndArticle FindNewsAndArticleById(long id);
        bool AddNewsAndArticle(NewsAndArticle newsAndArticle);
        bool UpdateNewsAndArticle(NewsAndArticle newsAndArticle);
        void DeleteNewsAndArticle(NewsAndArticle newsAndArticle);
        bool Delete(long id);
        NewsAndArticleResponse GetNewsAndArticleList(NewsAndArticleSearchRequest request, bool type);
    }
}
