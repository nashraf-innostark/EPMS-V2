using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Web.Models.Common;

namespace EPMS.Web.ModelMappers
{
    public static class WarehouseMapper
    {
        public static Models.Warehouse CreateFromServerToClient(this Warehouse source)
        {
            return new Models.Warehouse
            {
                WarehouseId = source.WarehouseId,
                WarehouseNumber = source.WarehouseNumber,
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
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
        public static Warehouse CreateFromClientToServer(this Models.Warehouse source)
        {
            var location = source.WarehouseLocation;
            location = location.Replace("\r", "");
            location = location.Replace("\t", "");
            location = location.Replace("\n", "");
            return new Warehouse
            {
                WarehouseId = source.WarehouseId,
                WarehouseNumber = source.WarehouseNumber,
                WarehouseManager = source.WarehouseManager,
                WarehouseSize = source.WarehouseSize,
                IsFull = source.IsFull,
                WarehouseLocation = location,
                ParentId = source.ParentId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
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
    }
}