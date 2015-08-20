namespace EPMS.WebModels.ModelMappers.Website.AboutUs
{
    public static class AboutUsMapper
    {
        public static WebsiteModels.AboutUs CreateFromServerToClient(this Models.DomainModels.AboutUs source)
        {
            return new WebsiteModels.AboutUs
            {
                AboutUsId = source.AboutUsId,
                Title = source.Title,
                TitleAr = source.TitleAr,
                ContentAr = source.ContentAr,
                ContentEn = source.ContentEn,
                MetaDesc = source.MetaDesc,
                MetaDescAr = source.MetaDescAr,
                MetaKeywords = source.MetaKeywords,
                MetaKeywordsAr = source.MetaKeywordsAr,
                ShowToPublic = source.ShowToPublic,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }

        public static Models.DomainModels.AboutUs CreateFromClientToServer(this WebsiteModels.AboutUs source)
        {
            return new Models.DomainModels.AboutUs
            {
                AboutUsId = source.AboutUsId,
                Title = source.Title,
                TitleAr = source.TitleAr,
                ContentAr = source.ContentAr,
                ContentEn = source.ContentEn,
                MetaDesc = source.MetaDesc,
                MetaDescAr = source.MetaDescAr,
                MetaKeywords = source.MetaKeywords,
                MetaKeywordsAr = source.MetaKeywordsAr,
                ShowToPublic = source.ShowToPublic,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }

    }
}
