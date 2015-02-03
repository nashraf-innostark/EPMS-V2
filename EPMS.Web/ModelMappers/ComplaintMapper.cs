using System;
using System.Globalization;
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
                ComplaintDate = DateTime.ParseExact(source.ComplaintDate, "dd/MM/yyyy", new CultureInfo("en")),
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
                ClientName = source.Customer.CustomerNameE,
                Topic = source.Topic,
                Description = source.Description,
                ComplaintDesc = source.Description,
                Reply = source.Reply,
                IsReplied = source.IsReplied,
                IsRepliedString = source.IsReplied?"Yes":"No",
                Status = source.Status,
                ComplaintDate = source.ComplaintDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
        public static DashboardModels.Complaint CreateForDashboard(this Complaint source)
        {
            return new DashboardModels.Complaint
            {
                ComplaintId = source.ComplaintId,
                ClientName = source.Customer.CustomerNameE,
                Topic = source.Topic,
                ClientNameShort = source.Customer.CustomerNameE.Length > 7 ? source.Customer.CustomerNameE.Substring(0, 7) + "..." : source.Customer.CustomerNameE,
                TopicShort = source.Topic.Length > 9 ? source.Topic.Substring(0, 9) + "..." : source.Topic,
                IsReplied = source.IsReplied
            };
        }
        #endregion
    }
}