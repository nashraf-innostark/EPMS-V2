namespace EPMS.WebModels.ModelMappers.Website.NewsAndArticles
{
    public static class NewsAndArticleMapper
    {
        public static WebsiteModels.NewsAndArticle CreateFromServerToClient(this Models.DomainModels.NewsAndArticle source)
        {
            return new WebsiteModels.NewsAndArticle
            {
                NewsArticleId = source.NewsArticleId,
                SortOrder = source.SortOrder,
                Type = source.Type,
                //True means Article, False News
                TypeForIndex = source.Type ? "Article" : "News",
                TitleEn = source.TitleEn,
                TitleAr = source.TitleAr,
                ImagePath = source.ImagePath,
                AuthorNameAr = source.AuthorNameAr,
                AuthorNameEn = source.AuthorNameEn,
                ContentAr =  source.ContentAr,
                ContentEn = source.ContentEn,
                ShowToPublic = source.ShowToPublic,
                MetaDesc = source.MetaDesc,
                MetaDescAr = source.MetaDescAr,
                MetaKeywords = source.MetaKeywords,
                MetaKeywordsAr = source.MetaKeywordsAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                DateTimeForIndex = source.RecCreatedDt.ToShortDateString()
            };
        }

        public static Models.DomainModels.NewsAndArticle CreateFromClientToServer(this WebsiteModels.NewsAndArticle source)
        {
            return new Models.DomainModels.NewsAndArticle
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
                MetaDesc = source.MetaDesc,
                MetaDescAr = source.MetaDescAr,
                MetaKeywords = source.MetaKeywords,
                MetaKeywordsAr = source.MetaKeywordsAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}