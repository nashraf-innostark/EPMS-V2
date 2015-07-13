using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ItemManufacturerMapper
    {
        public static WebModels.ItemManufacturer CreateFromServerToClient(this DomainModels.ItemManufacturer source)
        {
            return new WebModels.ItemManufacturer
            {
                ItemVariationId = source.ItemVariationId,
                ManufacturerId = source.ManufacturerId,
                Price = source.Price,
                ManufacturerNameEn = source.Manufacturer.ManufacturerNameEn,
                ManufacturerNameAr = source.Manufacturer.ManufacturerNameAr,
                Quantity = source.Quantity
            };
        }
    }
}