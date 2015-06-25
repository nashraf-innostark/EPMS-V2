using System;
using System.Globalization;
using System.Linq;
using EPMS.Web.DashboardModels;
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
                Status = source.Rfi.Status == 0 ?6 : source.Rfi.Status,
                NotesE = source.Rfi.NotesE,
                NotesA = source.Rfi.NotesA,
                ManagerId = source.Rfi.ManagerId,
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
                PlaceInDepartment = source.PlaceInDepartment,

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
                Status = source.Rfi.Status == 0 ? 6 : source.Rfi.Status,
                NotesE = source.Rfi.NotesE,
                NotesA = source.Rfi.NotesA,
                ManagerId = source.Rfi.ManagerId,
                RecCreatedBy = source.Rfi.RecCreatedBy,
                RecCreatedDate = source.Rfi.RecCreatedDate,
                RecUpdatedBy = source.Rfi.RecUpdatedBy,
                RecUpdatedDate = source.Rfi.RecUpdatedDate
            };
            return rfi;
        }
        public static EPMS.Models.DomainModels.RFI CreateRfiDetailsClientToServer(this Models.RFI source)
        {
            var rfi = new EPMS.Models.DomainModels.RFI
            {
                RFIId = source.RFIId,
                OrderId = source.OrderId,
                UsageE = source.UsageE,
                UsageA = source.UsageA,
                Status = source.Status == 0 ? 6 : source.Status,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
            return rfi;
        }
        #endregion

        #region Server to Client
        public static RFIWidget CreateRFIWidget(this EPMS.Models.DomainModels.RFI source)
        {
            var rfi = new RFIWidget
            {
                RFIId = source.RFIId,
                Status = source.Status,
                RequesterName = (source.AspNetUser != null && source.AspNetUser.Employee != null) ? source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE : string.Empty,
                RecCreatedDate = source.RecCreatedDate
            };
            return rfi;
        }

        public static Models.RFI CreateRfiServerToClient(this EPMS.Models.DomainModels.RFI source)
        {
            var rfi = new Models.RFI
            {
                RFIId = source.RFIId,
                OrderId = source.OrderId,
                UsageE = source.UsageE,
                UsageA = source.UsageA,
                ManagerId = source.ManagerId,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                Status = source.Status,
                RequesterName = (source.AspNetUser != null && source.AspNetUser.Employee != null) ? source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE : string.Empty,
                CustomerName = source.Order != null ? source.Order.Customer.CustomerNameE : string.Empty,
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
                PlaceInDepartment = source.PlaceInDepartment,
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
                PlaceInDepartment = source.PlaceInDepartment,
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

        #region Create For Dropdown List
        public static Models.RFI CreateRfiServerToClientForDropdown(this EPMS.Models.DomainModels.RFI source)
        {
            var rfi = new Models.RFI
            {
                RFIId = source.RFIId,
                OrderId = source.OrderId,
                //RequesterName = source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE,
                CustomerName = source.Order.Customer.CustomerNameE,
                OrderNumber = source.Order != null ? source.Order.OrderNo : string.Empty,
                CustomerDeliveryInfo = (source.Order != null && source.Order.Customer != null) ? source.Order.Customer.CustomerAddress : string.Empty,
            };
            return rfi;
        }
        #endregion
    }
}