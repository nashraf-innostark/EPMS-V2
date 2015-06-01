using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class WarehouseDetailMapper
    {
        public static Models.WarehouseDetail CreateFromServerToClient(this WarehouseDetail source)
        {
            return new Models.WarehouseDetail
            {
                WarehouseDetailId = source.WarehouseDetailId,
                WarehouseId = source.WarehouseId,
                NameEn = source.NameEn,
                NameAr = source.NameAr,
                NodeLevel = source.NodeLevel,
                ParentId = source.ParentId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }

        public static WarehouseDetail CreateFromClientToServer(this Models.WarehouseDetail source)
        {
            return new WarehouseDetail
            {
                WarehouseDetailId = source.WarehouseDetailId,
                WarehouseId = source.WarehouseId,
                NameEn = source.NameEn,
                NameAr = source.NameAr,
                NodeLevel = source.NodeLevel,
                ParentId = source.ParentId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}