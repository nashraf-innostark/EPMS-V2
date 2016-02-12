using System;
using System.Globalization;

namespace EPMS.WebModels.ModelMappers.Website.AboutUs
{
    public static class AboutUsMapper
    {
        public static WebsiteModels.AboutUs CreateFromServerToClient(this Models.DomainModels.AboutUs source)
        {
            string contentE = source.ContentEn.Replace("\r", "");
            contentE = contentE.Replace("\n", "");
            string contentA = source.ContentAr.Replace("\r", "");
            contentA = contentA.Replace("\n", "");
            return new WebsiteModels.AboutUs
            {
                AboutUsId = source.AboutUsId,
                Title = source.Title,
                TitleAr = source.TitleAr,
                ContentAr = contentA,
                ContentEn = contentE,
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
        }

        public static Models.DomainModels.AboutUs CreateFromClientToServer(this WebsiteModels.AboutUs source)
        {
            string contentE = source.ContentEn.Replace("\r", "");
            contentE = contentE.Replace("\n", "");
            string contentA = source.ContentAr.Replace("\r", "");
            contentA = contentA.Replace("\n", "");
            return new Models.DomainModels.AboutUs
            {
                AboutUsId = source.AboutUsId,
                Title = source.Title,
                TitleAr = source.TitleAr,
                ContentAr = contentA,
                ContentEn = contentE,
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
