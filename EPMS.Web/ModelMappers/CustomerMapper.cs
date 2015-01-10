using ApiModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class CustomerMapper
    {
        public static Models.Customer CreateFrom(this DomainModels.Customer source)
        {
            return new Models.Customer
            {

                CustomerId = source.CustomerId,
                CustomerName = source.CustomerName,
                CustomerAddress = source.CustomerAddress,
                CustomerMobile = source.CustomerMobile,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };

        }

        public static DomainModels.Customer CreateFrom(this Models.Customer source)
        {
            return new DomainModels.Customer
            {
                CustomerId = source.CustomerId,
                CustomerName = source.CustomerName,
                CustomerAddress = source.CustomerAddress,
                CustomerMobile = source.CustomerMobile,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };

        }
    }
}