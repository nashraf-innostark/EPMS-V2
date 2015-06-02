using System;
using System.Globalization;
using System.Linq;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.RIF;

namespace EPMS.Web.ModelMappers.Inventory.RIF
{
    public static class RifMapper
    {
        #region Client to Server
        public static EPMS.Models.DomainModels.RIF CreateRifClientToServer(this RIFViewModel source)
        {
            var rif = new EPMS.Models.DomainModels.RIF
            {
                RIFId = source.Rif.RIFId,
                OrderId = source.Rif.OrderId,
                ReturningReasonE = source.Rif.ReasonE,
                ReturningReasonA = source.Rif.ReasonA,
                Status = source.Rif.Status == 0 ? 2 : source.Rif.Status,
                NotesE = source.Rif.NotesE,
                NotesA = source.Rif.NotesA,

                RecCreatedBy = source.Rif.RecCreatedBy,
                RecCreatedDate = source.Rif.RecCreatedDate,
                RecUpdatedBy = source.Rif.RecUpdatedBy,
                RecUpdatedDate = source.Rif.RecUpdatedDate,

                RIFItems = source.RifItem.Select(x => x.CreateRifItemClientToServer(source.Rif.RIFId, source.Rif.RecCreatedBy, source.Rif.RecCreatedDate, source.Rif.RecUpdatedDate)).ToList()
            };
            return rif;
        }
        public static EPMS.Models.DomainModels.RIFItem CreateRifItemClientToServer(this RIFItem source, long rifId, string createdBy, DateTime createdDate, DateTime updatedDate)
        {
            var rifItem = new EPMS.Models.DomainModels.RIFItem
            {
                RIFItemId = source.RIFItemId,
                RIFId = rifId,
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
            return rifItem;
        }
        public static EPMS.Models.DomainModels.RIF CreateRifDetailsClientToServer(this RIFViewModel source)
        {
            var rif = new EPMS.Models.DomainModels.RIF
            {
                RIFId = source.Rif.RIFId,
                OrderId = source.Rif.OrderId,
                ReturningReasonE = source.Rif.ReasonE,
                ReturningReasonA = source.Rif.ReasonA,
                Status = source.Rif.Status == 0 ? 2 : source.Rif.Status,
                NotesE = source.Rif.NotesE,
                NotesA = source.Rif.NotesA,

                RecCreatedBy = source.Rif.RecCreatedBy,
                RecCreatedDate = source.Rif.RecCreatedDate,
                RecUpdatedBy = source.Rif.RecUpdatedBy,
                RecUpdatedDate = source.Rif.RecUpdatedDate
            };
            return rif;
        }
        #endregion

        #region Server to Client
        public static Models.RIF CreateRifServerToClient(this EPMS.Models.DomainModels.RIF source)
        {
            var rif = new Models.RIF
            {
                RIFId = source.RIFId,
                OrderId = source.OrderId,
                ReasonE = source.ReturningReasonE,
                ReasonA = source.ReturningReasonA,

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
            return rif;
        }

        public static RIFItem CreateRifItemServerToClient(this EPMS.Models.DomainModels.RIFItem source)
        {
            var rifItem = new RIFItem
            {
                RIFItemId = source.RIFItemId,
                RIFId = source.RIFId,
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
                rifItem.ItemVariationId = source.ItemVariationId;
                rifItem.ItemSKUCode = source.ItemVariation.SKUCode;
            }
            return rifItem;
        }
        #endregion

        #region Rif Item Detail Server to Client
        public static RIFItem CreateRifItemDetailsServerToClient(this EPMS.Models.DomainModels.RIFItem source)
        {
            var rifItem = new RIFItem
            {
                RIFItemId = source.RIFItemId,
                RIFId = source.RIFId,
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
                rifItem.ItemVariationId = source.ItemVariationId;
                rifItem.ItemSKUCode = source.ItemVariation.SKUCode;
                rifItem.ItemCode = source.ItemVariation.InventoryItem.ItemCode;
                rifItem.ItemName = Resources.Shared.Common.TextDirection == "ltr"
                    ? source.ItemVariation.InventoryItem.ItemNameEn
                    : source.ItemVariation.InventoryItem.ItemNameAr;
            }
            return rifItem;
        }
        #endregion
    }
}