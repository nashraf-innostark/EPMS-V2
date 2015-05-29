using System.Linq;
using EPMS.Models.DomainModels;

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
                NoOfAisles = source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x=>x.NodeLevel == 1) : 0,
                NoOfSections = source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 2) : 0,
                NoOfShalves = source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 3) : 0,
                NoOfSectoinsInShalves = source.WarehouseDetails.Any() ? source.WarehouseDetails.Count(x => x.NodeLevel == 4) : 0,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
        public static Warehouse CreateFromClientToServer(this Models.Warehouse source)
        {
            return new Warehouse
            {
                WarehouseId = source.WarehouseId,
                WarehouseNumber = source.WarehouseNumber,
                WarehouseManager = source.WarehouseManager,
                WarehouseSize = source.WarehouseSize,
                IsFull = source.IsFull,
                WarehouseLocation = source.WarehouseLocation,
                ParentId = source.ParentId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}