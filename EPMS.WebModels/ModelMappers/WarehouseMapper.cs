﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPMS.Models.DashboardModels;
using EPMS.Models.DomainModels;
using EPMS.WebModels.WebsiteModels.Common;

namespace EPMS.WebModels.ModelMappers
{
    public static class WarehouseMapper
    {
        public static WebsiteModels.Warehouse CreateFromServerToClient(this Warehouse source)
        {
            return new WebsiteModels.Warehouse
            {
                WarehouseId = source.WarehouseId,
                WarehouseNumber = source.WarehouseNumber,
                ManagerName = source.ManagerName,
                WarehouseManager = source.WarehouseManager,
                WarehouseSize = source.WarehouseSize,
                IsFull = source.IsFull,
                WarehouseLocation = source.WarehouseLocation,
                ParentId = source.ParentId,
                EmployeeNameEn = source.Employee != null ? source.Employee.EmployeeFirstNameE + " " + source.Employee.EmployeeMiddleNameE + " " + source.Employee.EmployeeLastNameE : string.Empty,
                EmployeeNameAr = source.Employee != null ? source.Employee.EmployeeFirstNameA + " " + source.Employee.EmployeeMiddleNameA + " " + source.Employee.EmployeeLastNameA : string.Empty,
                NoOfAisles = source.WarehouseDetails !=null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x=>x.NodeLevel == 1) : 0,
                NoOfSections = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 2) : 0,
                NoOfShalves = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 3) : 0,
                NoOfSectoinsInShalves = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 4) : 0,
                NoOfSpaces = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x=>x.NodeLevel == 5) : 0,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                WarehouseDetails = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Select(x => x.CreateFromServerToClient()).ToList() : new List<WebsiteModels.WarehouseDetail>()
            };
        }
        public static WebsiteModels.Warehouse CreateForReport(this Warehouse source)
        {
            return new WebsiteModels.Warehouse
            {
                WarehouseId = source.WarehouseId,
                WarehouseNumber = source.WarehouseNumber,
                WarehouseSize = source.WarehouseSize,
                IsFull = source.IsFull,
               
                NoOfAisles = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 1) : 0,
                NoOfSections = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 2) : 0,
                NoOfShalves = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 3) : 0,
                NoOfSectoinsInShalves = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 4) : 0,
                NoOfSpaces = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 5) : 0,
                
                WarehouseDetails = source.WarehouseDetails != null && source.WarehouseDetails.Any() ? source.WarehouseDetails.Select(x => x.CreateFromServerToClient()).ToList() : new List<WebsiteModels.WarehouseDetail>()
            };
        }
        public static WebsiteModels.Warehouse CreateFromItemVariationDropDown(this Warehouse source)
        {
            return new WebsiteModels.Warehouse
            {
                WarehouseId = source.WarehouseId,
                WarehouseNumber = source.WarehouseNumber,
                ManagerName = source.ManagerName,
                WarehouseManager = source.WarehouseManager,
                WarehouseSize = source.WarehouseSize,
                IsFull = source.IsFull,
                ParentId = source.ParentId,
            };
        }
        public static Warehouse CreateFromClientToServer(this WebsiteModels.Warehouse source)
        {
            var location = source.WarehouseLocation;
            if (!string.IsNullOrEmpty(location))
            {
                location = location.Replace("\r", "");
                location = location.Replace("\t", "");
                location = location.Replace("\n", "");
            }
            return new Warehouse
            {
                WarehouseId = source.WarehouseId,
                WarehouseNumber = source.WarehouseNumber,
                ManagerName = source.ManagerName,
                WarehouseManager = source.WarehouseManager,
                WarehouseSize = source.WarehouseSize,
                IsFull = source.IsFull,
                WarehouseLocation = location,
                ParentId = source.ParentId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = DateTime.ParseExact(source.RecCreatedDt, "dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }

        public static JsTree CreateForJsTree(this WarehouseDetail source)
        {
            return new JsTree
            {
                NodeId = source.WarehouseDetailId,
                ParentId = source.ParentId ?? 0,
                NodeTitleEn = source.NameEn,
                NodeTitleAr = source.NameAr,
            };
        }

        public static WebsiteModels.ItemWarehouse CreateForItemWarehouse(this ItemWarehouse source)
        {
            return new WebsiteModels.ItemWarehouse
            {
                ItemVariationId = source.ItemVariationId,
                PlaceInWarehouse = source.PlaceInWarehouse,
                Quantity = source.Quantity,
                WarehouseId = source.WarehouseId,
                WarehouseNo = source.Warehouse.WarehouseNumber
            };
        }

        public static WarehousDDL CreateDDL(this Warehouse source)
        {
            return new WarehousDDL
            {
                WarehouseId = source.WarehouseId,
                WarehouseNumber = source.WarehouseNumber
            };
        }
    }
}