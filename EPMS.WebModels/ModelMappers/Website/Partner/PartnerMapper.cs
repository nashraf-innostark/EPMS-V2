using System;
using System.Globalization;

namespace EPMS.WebModels.ModelMappers.Website.Partner
{
    public static class PartnerMapper
    {
        public static Models.DomainModels.Partner CreateFromClientToServer(this WebsiteModels.Partner source)
        {
            return new Models.DomainModels.Partner
            {
                PartnerId = source.PartnerId,
                NameEn = source.NameEn,
                NameAr = source.NameAr,
                ImageOrder = source.ImageOrder,
                ImageName = source.ImageName,
                Link = source.Link,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = !string.IsNullOrEmpty(source.RecCreatedDateStr) ? DateTime.ParseExact(source.RecCreatedDateStr, "dd/MM/yyyy", new CultureInfo("en")) : source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }

        public static WebsiteModels.Partner CreateFromServerToClient(this Models.DomainModels.Partner source)
        {
            return new WebsiteModels.Partner
            {
                PartnerId = source.PartnerId,
                NameEn = source.NameEn,
                NameAr = source.NameAr,
                ImageOrder = source.ImageOrder,
                ImageName = source.ImageName,
                Link = source.Link,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecCreatedDateStr = source.RecCreatedDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }
    }
}