using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers.WebsiteClient
{
    public static class WebsiteCustomerMapper
    {
        public static WebsiteModels.WebsiteCustomer CreateFromServerToClient(this WebsiteCustomer source)
        {
            return new WebsiteModels.WebsiteCustomer
            {
                CustomerId = source.CustomerId,
                CustomerNameEn = source.CustomerNameEn,
                CustomerNameAr = source.CustomerNameAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }

        public static WebsiteCustomer CreateFromClientToServer(this WebsiteModels.WebsiteCustomer source)
        {
            return new WebsiteCustomer
            {
                CustomerId = source.CustomerId,
                CustomerNameEn = source.CustomerNameEn,
                CustomerNameAr = source.CustomerNameAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }
    }
}
