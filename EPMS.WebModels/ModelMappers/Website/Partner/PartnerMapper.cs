namespace EPMS.WebModels.ModelMappers.Website.Partner
{
    public static class PartnerMapper
    {
        public static EPMS.Models.DomainModels.Partner CreateFromClientToServer(this WebsiteModels.Partner source)
        {
            return new EPMS.Models.DomainModels.Partner
            {
                PartnerId = source.PartnerId,
                NameEn = source.NameEn,
                NameAr = source.NameAr,
                ImageOrder = source.ImageOrder,
                ImageName = source.ImageName,
                Link = source.Link,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
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
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }
    }
}