using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class ItemReleaseQuantityMapper
    {
        public static WebsiteModels.ItemReleaseQuantity CreateFromServerToClient(this ItemReleaseQuantity source)
        {
            return new WebsiteModels.ItemReleaseQuantity
            {
                ItemReleaseQuantityId = source.ItemReleaseQuantityId,
                IRFDetailId = source.IRFDetailId,
                ItemVariationId = source.ItemVariationId,
                WarehouseId = source.WarehouseId,
                Quantity = source.Quantity,
                Warehouse = source.Warehouse.CreateFromServerToClient(),
            };
        }
    }
}