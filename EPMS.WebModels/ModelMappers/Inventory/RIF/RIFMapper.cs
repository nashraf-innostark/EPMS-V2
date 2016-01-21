using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPMS.Models.DashboardModels;
using EPMS.Models.DomainModels;
using EPMS.WebModels.ViewModels.RIF;
using RIFItem = EPMS.WebModels.WebsiteModels.RIFItem;

namespace EPMS.WebModels.ModelMappers.Inventory.RIF
{
    public static class RifMapper
    {
        #region Client to Server
        public static EPMS.Models.DomainModels.RIF CreateRifClientToServer(this RIFViewModel source)
        {
            var rif = new EPMS.Models.DomainModels.RIF
            {
                RIFId = source.Rif.RIFId,
                FormNumber = source.Rif.FormNumber,
                OrderId = source.Rif.OrderId,
                ReturningReasonE = source.Rif.ReasonE,
                ReturningReasonA = source.Rif.ReasonA,
                Status = source.Rif.Status == 0 ? 6 : source.Rif.Status,
                NotesE = source.Rif.NotesE,
                NotesA = source.Rif.NotesA,
                ManagerId = source.Rif.ManagerId,
                IRFId = source.Rif.IRFId,
                RecCreatedBy = source.Rif.RecCreatedBy,
                RecCreatedDate = DateTime.ParseExact(source.Rif.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = source.Rif.RecUpdatedBy,
                RecUpdatedDate = source.Rif.RecUpdatedDate,
            };
            rif.RIFItems = source.RifItem.Select(x => x.CreateRifItemClientToServer(rif.RIFId, rif.RecCreatedBy, rif.RecCreatedDate, rif.RecUpdatedDate)).ToList();
            return rif;
        }
        public static EPMS.Models.DomainModels.RIFItem CreateRifItemClientToServer(this WebsiteModels.RIFItem source, long rifId, string createdBy, DateTime createdDate, DateTime updatedDate)
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
                PlaceInDepartment = source.PlaceInDepartment,
                WarehouseId = source.WarehouseId,

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
                FormNumber = source.Rif.FormNumber,
                OrderId = source.Rif.OrderId,
                ReturningReasonE = source.Rif.ReasonE,
                ReturningReasonA = source.Rif.ReasonA,
                Status = source.Rif.Status == 0 ? 6 : source.Rif.Status,
                NotesE = source.Rif.NotesE,
                NotesA = source.Rif.NotesA,
                ManagerId = source.Rif.ManagerId,
                IRFId = source.Rif.IRFId,
                RecCreatedBy = source.Rif.RecCreatedBy,
                RecCreatedDate = DateTime.ParseExact(source.Rif.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = source.Rif.RecUpdatedBy,
                RecUpdatedDate = source.Rif.RecUpdatedDate
            };
            return rif;
        }
        public static EPMS.Models.DomainModels.RIF CreateRifDetailsClientToServer(this WebsiteModels.RIF source)
        {
            var rif = new EPMS.Models.DomainModels.RIF
            {
                RIFId = source.RIFId,
                FormNumber = source.FormNumber,
                OrderId = source.OrderId,
                ReturningReasonE = source.ReasonE,
                ReturningReasonA = source.ReasonA,
                Status = source.Status == 0 ? 6 : source.Status,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                IRFId = source.IRFId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = DateTime.ParseExact(source.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
            return rif;
        }
        #endregion

        #region Server to Client
        public static RIFWidget CreateWidget(this EPMS.Models.DomainModels.RIF source)
        {
            var empName = (source.AspNetUser != null && source.AspNetUser.Employee != null)
                ? source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE +
                  " " + source.AspNetUser.Employee.EmployeeLastNameE
                : string.Empty;
            var rfi = new RIFWidget
            {
                Id = source.RIFId,
                FormNumber = source.FormNumber,
                Status = source.Status,
                RequesterName = empName,
                RequesterNameShort = empName.Length > 7 ? empName.Substring(0, 7) : empName,
                RecCreatedDate = source.RecCreatedDate.ToShortDateString()
            };
            return rfi;
        }
        public static WebsiteModels.RIF CreateRifServerToClient(this EPMS.Models.DomainModels.RIF source)
        {
            var rif = new WebsiteModels.RIF
            {
                RIFId = source.RIFId,
                FormNumber = source.FormNumber,
                OrderId = source.OrderId,
                ReasonE = source.ReturningReasonE,
                ReasonA = source.ReturningReasonA,
                ManagerId = source.ManagerId,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                Status = source.Status,
                IRFId = source.IRFId,
                RequesterName = source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE,
                CustomerName = source.Order.Customer.CustomerNameE,
                RecCreatedDateString = Convert.ToDateTime(source.RecCreatedDate).ToString("dd/MM/yyyy", new CultureInfo("en")) + "-" + Convert.ToDateTime(source.RecCreatedDate).ToString("dd/MM/yyyy", new CultureInfo("ar")),
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate.ToString("dd/MM/yyyy", new CultureInfo("en")),

                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
            return rif;
        }

        public static WebsiteModels.RIFItem CreateRifItemServerToClient(this EPMS.Models.DomainModels.RIFItem source, List<WebsiteModels.ItemWarehouse> itemWarehouses)
        {
            var rifItem = new WebsiteModels.RIFItem
            {
                RIFItemId = source.RIFItemId,
                RIFId = source.RIFId,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemDetails = source.ItemDetails,
                PlaceInDepartment = source.PlaceInDepartment,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                WarehouseId = source.WarehouseId,
                ItemWarehouses = itemWarehouses.Where(x=>x.ItemVariationId == source.ItemVariationId)
            };
            if (source.ItemVariation != null)
            {
                rifItem.ItemVariationId = source.ItemVariationId;
                rifItem.ItemSKUCode = source.ItemVariation.SKUCode;
            }
            ItemReleaseDetail itemReleaseDetail =
                source.RIF.ItemRelease != null ? source.RIF.ItemRelease.ItemReleaseDetails.FirstOrDefault(x => x.ItemVariationId == source.ItemVariationId) : null;
            if (itemReleaseDetail != null)
                rifItem.ReleasedQty = itemReleaseDetail.ItemQty;

            return rifItem;
        }
        #endregion

        #region Rif Item Detail Server to Client
        public static WebsiteModels.RIFItem CreateRifItemDetailsServerToClient(this EPMS.Models.DomainModels.RIFItem source)
        {
            var rifItem = new WebsiteModels.RIFItem
            {
                RIFItemId = source.RIFItemId,
                RIFId = source.RIFId,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemDetails = source.ItemDetails,
                PlaceInDepartment = source.PlaceInDepartment,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                WarehouseId = source.WarehouseId
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