using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class EmployeeRequestMapper
    {
        public static EmployeeRequest MapClientToServer(this Models.EmployeeRequest source)
        {
            return new EmployeeRequest
            {
               RequestId = source.RequestId,
               EmployeeId = source.EmployeeId,
               IsMonetary = source.IsMonetary,
               RequestTopic = source.RequestTopic,
               RequestDate = source.RequestDate,
               RecCreatedBy = source.RecCreatedBy,
               RecCreatedDt = source.RecCreatedDt,
               RecLastUpdatedBy = source.RecLastUpdatedBy,
               RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        public static Models.EmployeeRequest MapServerToClient(this EmployeeRequest source)
        {
            return new Models.EmployeeRequest
            {
                RequestId = source.RequestId,
                EmployeeId = source.EmployeeId,
                IsMonetary = source.IsMonetary,
                RequestTopic = source.RequestTopic,
                RequestDate = source.RequestDate,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
    }
}