namespace EPMS.Web.ModelMappers.Website.Partner
{
    public static class PartnerMapper
    {
        public static EPMS.Models.DomainModels.Partner CreateFromClientToServer(this Models.Partner source)
        {
            string descp = source.Description.Replace("\n", "");
            descp = descp.Replace("\r", "");
            return new EPMS.Models.DomainModels.Partner
            {
                PartnerId = source.PartnerId,
                NameEn = source.NameEn,
                NameAr = source.NameAr,
                ImageOrder = source.ImageOrder,
                ImageName = source.ImageName,
                Link = source.Link,
                Description = descp,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }

        public static Models.Partner CreateFromServerToClient(this EPMS.Models.DomainModels.Partner source)
        {
            return new Models.Partner
            {
                PartnerId = source.PartnerId,
                NameEn = source.NameEn,
                NameAr = source.NameAr,
                ImageOrder = source.ImageOrder,
                ImageName = source.ImageName,
                Link = source.Link,
                Description = source.Description,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }
    }
}