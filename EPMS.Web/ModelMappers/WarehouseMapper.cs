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
                NoOfAisles = source.NoOfAisles,
                NoOfSections = source.NoOfSections,
                NoOfShalves = source.NoOfShalves,
                NoOfSectoinsInShalves = source.NoOfSectoinsInShalves,
                NoOfSpaces = source.NoOfSpaces,
                WarehouseLocation = source.WarehouseLocation,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                EmployeeNameEn = source.Employee != null ? source.Employee.EmployeeFirstNameE + " " + source.Employee.EmployeeMiddleNameE + " " + source.Employee.EmployeeLastNameE : string.Empty,
                EmployeeNameAr = source.Employee != null ? source.Employee.EmployeeFirstNameA + " " + source.Employee.EmployeeMiddleNameA + " " + source.Employee.EmployeeLastNameA : string.Empty,
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
                NoOfAisles = source.NoOfAisles,
                NoOfSections = source.NoOfSections,
                NoOfShalves = source.NoOfShalves,
                NoOfSectoinsInShalves = source.NoOfSectoinsInShalves,
                NoOfSpaces = source.NoOfSpaces,
                WarehouseLocation = source.WarehouseLocation,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
    }
}