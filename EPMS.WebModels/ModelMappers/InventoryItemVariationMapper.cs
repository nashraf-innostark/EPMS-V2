namespace EPMS.WebModels.ModelMappers
{
    public static class InventoryItemVariationMapper
    {
        public static WebsiteModels.InventoryItemForVariation CreateForItemVariation(this Models.DomainModels.InventoryItem source)
        {
            return new WebsiteModels.InventoryItemForVariation
            {
                ItemNameEn = source.ItemNameEn,
                ItemNameAr = source.ItemNameAr,
                InventoryDepartment = source.InventoryDepartment.CreateFromServerToClient()
            };
        }
    }
}