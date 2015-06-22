using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ItemReleaseQuantityMapper
    {
        public static Models.ItemReleaseQuantity CreateFromServerToClient(this ItemReleaseQuantity source)
        {
            return new Models.ItemReleaseQuantity
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