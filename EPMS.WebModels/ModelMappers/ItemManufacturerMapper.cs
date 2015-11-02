namespace EPMS.WebModels.ModelMappers
{
    public static class ItemManufacturerMapper
    {
        public static WebsiteModels.ItemManufacturer CreateFromServerToClient(this Models.DomainModels.ItemManufacturer source)
        {
            return new WebsiteModels.ItemManufacturer
            {
                ItemVariationId = source.ItemVariationId,
                ManufacturerId = source.ManufacturerId,
                Price = source.Price,
                ManufacturerNameEn = source.Vendor.VendorNameEn,
                ManufacturerNameAr = source.Vendor.VendorNameAr,
                Quantity = source.Quantity
            };
        }
    }
}