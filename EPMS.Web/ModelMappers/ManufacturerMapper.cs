using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ManufacturerMapper
    {
        public static WebModels.Manufacturer CreateFromServerToClient(this DomainModels.Manufacturer source)
        {
            return new WebModels.Manufacturer
            {
                ManufacturerId = source.ManufacturerId,
                ManufacturerNameEn = source.ManufacturerNameEn,
                ManufacturerNameAr = source.ManufacturerNameAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}