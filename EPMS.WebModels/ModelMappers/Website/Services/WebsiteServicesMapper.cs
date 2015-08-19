using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;
using EPMS.WebModels.WebsiteModels.Common;

namespace EPMS.WebModels.ModelMappers.Website.Services
{
    public static class WebsiteServicesMapper
    {
        public static WebsiteModels.WebsiteService CreateFromServerToClient(this WebsiteService source)
        {
            var retVal = new WebsiteModels.WebsiteService
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
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                NoofSections = source.WebsiteServices.Any() ? source.WebsiteServices.Count : 0,
                ParentService = source.ParentService != null ? source.ParentService.CreateFromServerToClient() : null
            };
            var parent = source.ParentService;
            while (parent != null && parent.ParentService != null)
            {
                parent = parent.ParentService;
            }
            retVal.ParentServiceEn = parent != null ? parent.ServiceNameEn : "None";
            retVal.ParentServiceAr = parent != null ? parent.ServiceNameAr : "None";
            return retVal;
        }

        public static WebsiteService CreateFromClientToServer(this WebsiteModels.WebsiteService source)
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
                parent = source.ParentServiceId != null ? source.ParentServiceId.ToString() : "parentNode"
            };
        }
    }
}
