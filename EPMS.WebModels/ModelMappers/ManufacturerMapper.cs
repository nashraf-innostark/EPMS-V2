using System.Globalization;

namespace EPMS.WebModels.ModelMappers
{
    public static class ManufacturerMapper
    {
        public static WebsiteModels.Manufacturer CreateFromServerToClient(this Models.DomainModels.Manufacturer source)
        {
            return new WebsiteModels.Manufacturer
            {
                ManufacturerId = source.ManufacturerId,
                ManufacturerNameEn = source.ManufacturerNameEn,
                ManufacturerNameAr = source.ManufacturerNameAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}