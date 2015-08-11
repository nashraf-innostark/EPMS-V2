using System;
using System.Globalization;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class WarehouseDetailMapper
    {
        public static WebsiteModels.WarehouseDetail CreateFromServerToClient(this WarehouseDetail source)
        {
            return new WebsiteModels.WarehouseDetail
            {
                WarehouseDetailId = source.WarehouseDetailId,
                WarehouseId = source.WarehouseId,
                NameEn = source.NameEn,
                NameAr = source.NameAr,
                NodeLevel = source.NodeLevel,
                ParentId = source.ParentId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = Convert.ToDateTime(source.RecCreatedDt).ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }

        public static WarehouseDetail CreateFromClientToServer(this WebsiteModels.WarehouseDetail source)
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
                RecCreatedDt = DateTime.ParseExact(source.RecCreatedDt, "dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}