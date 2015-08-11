using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class ComplaintMapper
    {
        #region Complaint Mappers
        public static Complaint CreateFromClientToServer(this WebsiteModels.Complaint source)
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
        public static WebsiteModels.Complaint CreateFromServerToClient(this Complaint source)
        {
            return new WebsiteModels.Complaint
            {
                ComplaintId = source.ComplaintId,
                CustomerId = source.CustomerId,
                DepartmentId = source.DepartmentId,
                OrderId = source.OrderId ?? 0,
                ClientName = source.Customer.CustomerNameE,
                Topic = source.Topic,
                Description = source.Description,
                ComplaintDesc = source.Description,
                Reply = source.Reply,
                IsReplied = source.IsReplied,
                IsRepliedString = source.IsReplied?"Yes":"No",
                Status = source.Status,
                ComplaintDate = source.ComplaintDate,
                ComplaintDateString = source.ComplaintDate.ToShortDateString(),
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
        
        #endregion
    }
}