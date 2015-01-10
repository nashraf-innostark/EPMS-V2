using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ComplaintMapper
    {
        #region Complaint Mappers
        public static Complaint CreateFromClientToServer(this Models.Complaint source)
        {
            return new Complaint
            {
                ComplaintId = source.ComplaintId,
                CustomerId = source.CustomerId,
                DepartmentId = source.DepartmentId,
                OrderId = source.OrderId,
                Topic = source.Topic,
                Description = source.Description,
                Reply = source.Reply,
                IsReplied = source.IsReplied,
                Status = source.Status,
                ComplaintDate = source.ComplaintDate,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        public static Models.Complaint CreateFromServerToClient(this Complaint source)
        {
            return new Models.Complaint
            {
                ComplaintId = source.ComplaintId,
                CustomerId = source.CustomerId,
                DepartmentId = source.DepartmentId,
                OrderId = source.OrderId,
                ClientName = source.Customer.CustomerName,
                Topic = source.Topic,
                Description = source.Description,
                ComplaintDesc = source.Description,
                Reply = source.Reply,
                IsReplied = source.IsReplied,
                Status = source.Status,
                ComplaintDate = source.ComplaintDate,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
        #endregion
    }
}