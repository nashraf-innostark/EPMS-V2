using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class IRQMapper
    {
        public static ItemReleaseQuantity CreateFromServerToClient(this Models.ItemReleaseQuantity source, long IRFDetailId)
        {
            return new ItemReleaseQuantity
            {
                ItemReleaseQuantityId = source.ItemReleaseQuantityId,
                IRFDetailId = IRFDetailId,
                ItemVariationId = source.ItemVariationId,
                WarehouseId = source.WarehouseId,
                Quantity = source.Quantity,
            };
        }
    }
}