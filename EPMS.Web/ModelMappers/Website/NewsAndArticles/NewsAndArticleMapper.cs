using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.Website.NewsAndArticles
{
    public static class NewsAndArticleMapper
    {
        public static WebModels.NewsAndArticle CreateFromServerToClient(this DomainModels.NewsAndArticle source)
        {
            return new WebModels.NewsAndArticle
            {
                NewsArticleId = source.NewsArticleId,
                SortOrder = source.SortOrder,
                Type = source.Type,
                TypeForIndex = source.Type ? "News" : "Article",
                TitleEn = source.TitleEn,
                TitleAr = source.TitleAr,
                ImagePath = source.ImagePath,
                AuthorNameAr = source.AuthorNameAr,
                AuthorNameEn = source.AuthorNameEn,
                ContentAr =  source.ContentAr,
                ContentEn = source.ContentEn,
                ShowToPublic = source.ShowToPublic,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                DateTimeForIndex = source.RecCreatedDt.ToShortDateString()
            };
        }

        public static DomainModels.NewsAndArticle CreateFromClientToServer(this WebModels.NewsAndArticle source)
        {
            return new DomainModels.NewsAndArticle
            {
                NewsArticleId = source.NewsArticleId,
                SortOrder = source.SortOrder,
                Type = source.Type,
                TitleEn = source.TitleEn,
                TitleAr = source.TitleAr,
                ImagePath = source.ImagePath,
                AuthorNameAr = source.AuthorNameAr,
                AuthorNameEn = source.AuthorNameEn,
                ContentAr = source.ContentAr,
                ContentEn = source.ContentEn,
                ShowToPublic = source.ShowToPublic,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}