using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class IRQMapper
    {
        public static ItemReleaseQuantity CreateFromServerToClient(this WebsiteModels.ItemReleaseQuantity source, long IRFDetailId)
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