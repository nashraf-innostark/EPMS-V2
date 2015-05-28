using System;
using System.Linq;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.RFI;
namespace EPMS.Web.ModelMappers.Inventory.RFI
{
    public static class RfiMapper
    {
        public static EPMS.Models.DomainModels.RFI CreateRfiClientToServer(this RFIViewModel source)
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

                RFIItems = source.RfiItem.Select(x => x.CreateRfiItemClientToServer(source.Rfi.RecCreatedBy, source.Rfi.RecCreatedDate, source.Rfi.RecUpdatedDate)).ToList()
            };
            return rfi;
        }

        public static EPMS.Models.DomainModels.RFIItem CreateRfiItemClientToServer(this RFIItem source, string createdBy, DateTime createdDate, DateTime updatedDate)
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

        public static Models.RFI CreateRfiServerToClient(this EPMS.Models.DomainModels.RFI source)
        {
            var rfi = new Models.RFI
            {
                RFIId = source.RFIId,
                OrderId = source.OrderId,
                UsageE = source.UsageE,
                UsageA = source.UsageA,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecCreatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
            return rfi;
        }

        public static RFIItem CreateRfiItemServerToClient(this EPMS.Models.DomainModels.RFIItem source)
        {
            var rfiItem = new RFIItem
            {
                RFIItemId = source.RFIItemId,
                RFIId = source.RFIId,
                ItemVariationId = source.ItemVariationId,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemDetails = source.ItemDetails,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
            return rfiItem;
        }
    }
}