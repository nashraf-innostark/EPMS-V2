using System;
using System.Linq;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.RFI;
namespace EPMS.Web.ModelMappers.Inventory.RFI
{
    public static class RFIMapper
    {
        public static EPMS.Models.DomainModels.RFI CreateRFIClientToServer(this RFIViewModel source)
        {
            var rfi = new EPMS.Models.DomainModels.RFI
            {
                RFIId = source.Rfi.RFIId,
                OrderId = source.Rfi.OrderId,
                UsageE = source.Rfi.UsageE,
                UsageA = source.Rfi.UsageA,
                RecCreatedBy = source.Rfi.RecCreatedBy,
                RecCreatedDate = source.Rfi.RecCreatedDate,
                RecUpdatedBy = source.Rfi.RecCreatedBy,
                RecUpdatedDate = source.Rfi.RecUpdatedDate,

                RFIItems = source.RfiItem.Select(x => x.CreateRFIItemClientToServer(source.Rfi.RecCreatedBy, source.Rfi.RecCreatedDate, source.Rfi.RecUpdatedDate)).ToList()
            };
            return rfi;
        }

        public static EPMS.Models.DomainModels.RFIItem CreateRFIItemClientToServer(this RFIItem source, string createdBy, DateTime createdDate, DateTime updatedDate)
        {
            var rfiItem = new EPMS.Models.DomainModels.RFIItem
            {
                RFIItemId = source.RFIItemId,
                RFIId = source.RFIId,
                ItemVariationId = source.ItemVariationId,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemDetails = source.ItemDetails,

                RecCreatedBy = createdBy,
                RecCreatedDate = createdDate,
                RecUpdatedBy = createdBy,
                RecUpdatedDate = updatedDate
            };
            return rfiItem;
        }
    }
}