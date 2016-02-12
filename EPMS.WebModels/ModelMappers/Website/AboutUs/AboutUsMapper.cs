using System;
using System.Globalization;

namespace EPMS.WebModels.ModelMappers.Website.AboutUs
{
    public static class AboutUsMapper
    {
        public static WebsiteModels.AboutUs CreateFromServerToClient(this Models.DomainModels.AboutUs source)
        {
            var aboutUs =  new WebsiteModels.AboutUs
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
                RecCreatedDt = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };

            var infoEn = source.ContentAr;
            if (!string.IsNullOrEmpty(infoEn))
            {
                infoEn = infoEn.Replace("\r", "");
                infoEn = infoEn.Replace("\t", "");
                infoEn = infoEn.Replace("\n", "");
            }
            aboutUs.ContentEn = infoEn;

            var infoAr = source.ContentAr;
            if (!string.IsNullOrEmpty(infoAr))
            {
                infoAr = infoAr.Replace("\r", "");
                infoAr = infoAr.Replace("\t", "");
                infoAr = infoAr.Replace("\n", "");
            }
            aboutUs.ContentAr = infoAr;

            return aboutUs;
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
                RecCreatedDt = DateTime.ParseExact(source.RecCreatedDt, "dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }

    }
}
