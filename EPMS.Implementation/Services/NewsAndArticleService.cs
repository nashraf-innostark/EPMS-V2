using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class NewsAndArticleService : INewsAndArticleService
    {

        #region Private

        private readonly INewsAndArticleRepository newsAndArticleRepository;

        #endregion

        #region Constructor

        public NewsAndArticleService(INewsAndArticleRepository newsAndArticleRepository)
        {
            this.newsAndArticleRepository = newsAndArticleRepository;
        }

        #endregion

        #region Public

        public IEnumerable<NewsAndArticle> GetAll()
        {
            return newsAndArticleRepository.GetAll();
        }

        public NewsAndArticle FindNewsAndArticleById(long id)
        {
            return newsAndArticleRepository.Find(id);
        }

        public bool AddNewsAndArticle(NewsAndArticle newsAndArticle)
        {
            newsAndArticleRepository.Add(newsAndArticle);
            newsAndArticleRepository.SaveChanges();
            return true;
        }

        public bool UpdateNewsAndArticle(NewsAndArticle newsAndArticle)
        {
            newsAndArticleRepository.Update(newsAndArticle);
            newsAndArticleRepository.SaveChanges();
            return true;
        }

        public void DeleteNewsAndArticle(NewsAndArticle newsAndArticle)
        {
            newsAndArticleRepository.Delete(newsAndArticle);
            newsAndArticleRepository.SaveChanges();
        }

        public bool Delete(long id)
        {
            NewsAndArticle newsAndArticleToDelete = newsAndArticleRepository.Find(id);
            newsAndArticleRepository.Delete(newsAndArticleToDelete);
            newsAndArticleRepository.SaveChanges();
            return true;
        }

        public NewsAndArticleResponse GetNewsAndArticleList(NewsAndArticleSearchRequest request, bool type)
        {
            NewsAndArticleResponse response = new NewsAndArticleResponse
            {
                NewsAndArticles = new List<NewsAndArticle>()
            };
            return newsAndArticleRepository.GetNewsAndArticleList(request, type);
            //response.NewsAndArticles = newsAndArticles.NewsAndArticles;
            //response.TotalCount = newsAndArticles.TotalCount;
            //return newsAndArticles;
        }

        #endregion
    }
}
