using System;
using System.Globalization;
using System.Linq;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.RFI;
namespace EPMS.Web.ModelMappers.Inventory.RFI
{
    public static class RfiMapper
    {
        #region Client to Server
        public static EPMS.Models.DomainModels.RFI CreateRfiClientToServer(this RFIViewModel source)
        {
            var rfi = new EPMS.Models.DomainModels.RFI
            {
                RFIId = source.Rfi.RFIId,
                OrderId = source.Rfi.OrderId,
                UsageE = source.Rfi.UsageE,
                UsageA = source.Rfi.UsageA,
                Status = source.Rfi.Status == 0 ? 2 : source.Rfi.Status,
                NotesE = source.Rfi.NotesE,
                NotesA = source.Rfi.NotesA,

                RecCreatedBy = source.Rfi.RecCreatedBy,
                RecCreatedDate = source.Rfi.RecCreatedDate,
                RecUpdatedBy = source.Rfi.RecUpdatedBy,
                RecUpdatedDate = source.Rfi.RecUpdatedDate,

                RFIItems = source.RfiItem.Select(x => x.CreateRfiItemClientToServer(source.Rfi.RFIId, source.Rfi.RecCreatedBy, source.Rfi.RecCreatedDate, source.Rfi.RecUpdatedDate)).ToList()
            };
            return rfi;
        }

        public static EPMS.Models.DomainModels.RFIItem CreateRfiItemClientToServer(this RFIItem source, long rfiId, string createdBy, DateTime createdDate, DateTime updatedDate)
        {
            var rfiItem = new EPMS.Models.DomainModels.RFIItem
            {
                RFIItemId = source.RFIItemId,
                RFIId = rfiId,
                ItemVariationId = source.ItemVariationId == 0 ? null : source.ItemVariationId,
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
        public static EPMS.Models.DomainModels.RFI CreateRfiDetailsClientToServer(this RFIViewModel source)
        {
            var rfi = new EPMS.Models.DomainModels.RFI
            {
                RFIId = source.Rfi.RFIId,
                OrderId = source.Rfi.OrderId,
                UsageE = source.Rfi.UsageE,
                UsageA = source.Rfi.UsageA,
                Status = source.Rfi.Status == 0 ? 2 : source.Rfi.Status,
                NotesE = source.Rfi.NotesE,
                NotesA = source.Rfi.NotesA,

                RecCreatedBy = source.Rfi.RecCreatedBy,
                RecCreatedDate = source.Rfi.RecCreatedDate,
                RecUpdatedBy = source.Rfi.RecUpdatedBy,
                RecUpdatedDate = source.Rfi.RecUpdatedDate
            };
            return rfi;
        }
        #endregion

        #region Server to Client
        public static Models.RFI CreateRfiServerToClient(this EPMS.Models.DomainModels.RFI source)
        {
            var rfi = new Models.RFI
            {
                RFIId = source.RFIId,
                OrderId = source.OrderId,
                UsageE = source.UsageE,
                UsageA = source.UsageA,

                NotesE = source.NotesE,
                NotesA = source.NotesA,
                Status = source.Status,
                RequesterName = source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE,
                CustomerName = source.Order.Customer.CustomerNameE,
                RecCreatedDateString = Convert.ToDateTime(source.RecCreatedDate).ToString("dd/MM/yyyy", new CultureInfo("en")) + "-" + Convert.ToDateTime(source.RecCreatedDate).ToString("dd/MM/yyyy", new CultureInfo("ar")),
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,

                RecUpdatedBy = source.RecUpdatedBy,
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
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemDetails = source.ItemDetails,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
            if (source.ItemVariation != null)
            {
                rfiItem.ItemVariationId = source.ItemVariationId;
                rfiItem.ItemSKUCode = source.ItemVariation.SKUCode;
            }
            return rfiItem;
        }
        #endregion

        #region Rfi Item Detail Server to Client
        public static RFIItem CreateRfiItemDetailsServerToClient(this EPMS.Models.DomainModels.RFIItem source)
        {
            var rfiItem = new RFIItem
            {
                RFIItemId = source.RFIItemId,
                RFIId = source.RFIId,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemDetails = source.ItemDetails,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
            if (source.ItemVariation != null)
            {
                rfiItem.ItemVariationId = source.ItemVariationId;
                rfiItem.ItemSKUCode = source.ItemVariation.SKUCode;
                rfiItem.ItemCode = source.ItemVariation.InventoryItem.ItemCode;
                rfiItem.ItemName = Resources.Shared.Common.TextDirection == "ltr"
                    ? source.ItemVariation.InventoryItem.ItemNameEn
                    : source.ItemVariation.InventoryItem.ItemNameAr;
            }
            return rfiItem;
        }
        #endregion
    }
}