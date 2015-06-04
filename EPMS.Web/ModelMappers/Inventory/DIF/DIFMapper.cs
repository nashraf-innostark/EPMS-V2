using System;
using System.Globalization;
using System.Linq;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.DIF;

namespace EPMS.Web.ModelMappers.Inventory.DIF
{
    public static class DifMapper
    {
        #region Client to Server
        public static EPMS.Models.DomainModels.DIF CreateDifClientToServer(this DIFViewModel source)
        {
            var rif = new EPMS.Models.DomainModels.DIF
            {
                Id = source.Dif.Id,
                DefectivenessE = source.Dif.DefectivenessE,
                DefectivenessA = source.Dif.DefectivenessA,
                Status = source.Dif.Status == 0 ? 6 : source.Dif.Status,
                NotesE = source.Dif.NotesE,
                NotesA = source.Dif.NotesA,
                ManagerId = source.Dif.ManagerId,
                RecCreatedBy = source.Dif.RecCreatedBy,
                RecCreatedDate = source.Dif.RecCreatedDate,
                RecUpdatedBy = source.Dif.RecUpdatedBy,
                RecUpdatedDate = source.Dif.RecUpdatedDate,

                DIFItems = source.DifItem.Select(x => x.CreateDifItemClientToServer(source.Dif.Id, source.Dif.RecCreatedBy, source.Dif.RecCreatedDate, source.Dif.RecUpdatedDate)).ToList()
            };
            return rif;
        }
        public static EPMS.Models.DomainModels.DIFItem CreateDifItemClientToServer(this DIFItem source, long rifId, string createdBy, DateTime createdDate, DateTime updatedDate)
        {
            var rifItem = new EPMS.Models.DomainModels.DIFItem
            {
                ItemId = source.ItemId,
                DIFId = rifId,
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
        public static EPMS.Models.DomainModels.DIF CreateDifDetailsClientToServer(this DIFViewModel source)
        {
            var rif = new EPMS.Models.DomainModels.DIF
            {
                Id = source.Dif.Id,
                DefectivenessE = source.Dif.DefectivenessE,
                DefectivenessA = source.Dif.DefectivenessA,
                Status = source.Dif.Status == 0 ? 6 : source.Dif.Status,
                NotesE = source.Dif.NotesE,
                NotesA = source.Dif.NotesA,
                ManagerId = source.Dif.ManagerId,
                RecCreatedBy = source.Dif.RecCreatedBy,
                RecCreatedDate = source.Dif.RecCreatedDate,
                RecUpdatedBy = source.Dif.RecUpdatedBy,
                RecUpdatedDate = source.Dif.RecUpdatedDate
            };
            return rif;
        }
        #endregion

        #region Server to Client
        public static Models.DIF CreateDifServerToClient(this EPMS.Models.DomainModels.DIF source)
        {
            var rif = new Models.DIF
            {
                Id = source.Id,
                DefectivenessE = source.DefectivenessE,
                DefectivenessA = source.DefectivenessA,
                ManagerId = source.ManagerId,
                ManagerName = source.ManagerId!=null?Resources.Shared.Common.TextDirection=="ltr"?source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE:source.AspNetUser.Employee.EmployeeFirstNameA + " " + source.AspNetUser.Employee.EmployeeMiddleNameA + " " + source.AspNetUser.Employee.EmployeeLastNameA:"",
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                Status = source.Status,
                RequesterName = source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE,
                RecCreatedDateString = Convert.ToDateTime(source.RecCreatedDate).ToString("dd/MM/yyyy", new CultureInfo("en")) + "-" + Convert.ToDateTime(source.RecCreatedDate).ToString("dd/MM/yyyy", new CultureInfo("ar")),
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,

                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
            return rif;
        }

        public static DIFItem CreateDifItemServerToClient(this EPMS.Models.DomainModels.DIFItem source)
        {
            var rifItem = new DIFItem
            {
                ItemId = source.ItemId,
                DIFId = source.DIFId,
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

        #region Dif Item Detail Server to Client
        public static DIFItem CreateDifItemDetailsServerToClient(this EPMS.Models.DomainModels.DIFItem source)
        {
            var rifItem = new DIFItem
            {
                ItemId = source.ItemId,
                DIFId = source.DIFId,
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