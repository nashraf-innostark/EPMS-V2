using EPMS.Models.DomainModels;
using EPMS.Web.Models.Common;

namespace EPMS.Web.ModelMappers.Website.WebsiteServices
{
    public static class WebsiteServicesMapper
    {
        public static Models.WebsiteService CreateFromServerToClient(this WebsiteService source)
        {
            return new Models.WebsiteService
            {
                ServiceId = source.ServiceId,
                ParentServiceId = source.ParentServiceId,
                ServiceNameEn = source.ServiceNameEn,
                ServiceNameAr = source.ServiceNameAr,
                DescriptionEn = source.DescriptionEn,
                DescriptionAr = source.DescriptionAr,
                MetaKeywordsEn = source.MetaKeywordsEn,
                MetaKeywordsAr = source.MetaKeywordsAr,
                MetaDescriptionEn = source.MetaDescriptionEn,
                MetaDescriptionAr = source.MetaDescriptionAr,
                ShowToPublic = source.ShowToPublic,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecCreatedDateStr = source.RecCreatedDate.ToShortDateString(),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
                
            };
        }

        public static WebsiteService CreateFromClientToServer(this Models.WebsiteService source)
        {
            string descpEn = source.DescriptionEn;
            if (!string.IsNullOrEmpty(descpEn))
            {
                descpEn = descpEn.Replace("\n", "");
                descpEn = descpEn.Replace("\r", "");
            }
            string descpAr = source.DescriptionAr;
            if (!string.IsNullOrEmpty(descpAr))
            {
                descpAr = descpAr.Replace("\n", "");
                descpAr = descpAr.Replace("\r", "");
            }
            return new WebsiteService
            {
                ServiceId = source.ServiceId,
                ParentServiceId = source.ParentServiceId,
                ServiceNameEn = source.ServiceNameEn,
                ServiceNameAr = source.ServiceNameAr,
                DescriptionEn = descpEn,
                DescriptionAr = descpAr,
                MetaKeywordsEn = source.MetaKeywordsEn,
                MetaKeywordsAr = source.MetaKeywordsAr,
                MetaDescriptionEn = source.MetaDescriptionEn,
                MetaDescriptionAr = source.MetaDescriptionAr,
                ShowToPublic = source.ShowToPublic,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static JsTreeJson CreateForJsTree(this WebsiteService source, string direction)
        {
            return new JsTreeJson
            {
                id = source.ServiceId.ToString(),
                text = direction == "ltr" ? source.ServiceNameEn : source.ServiceNameAr,
                parent = source.ParentServiceId !=null ? source.ParentServiceId.ToString() : "parentNode"
            };
        }
    }
}