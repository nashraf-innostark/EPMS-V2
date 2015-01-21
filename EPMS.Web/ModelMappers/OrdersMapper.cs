using System;
using System.Activities.Expressions;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class OrdersMapper
    {
        public static Order CreateFromClientToServer(this Models.Order source)
        {
            var caseType = new Order
            {
                OrderId = source.OrderId,
                OrderNo = source.OrderNo,
                OrderDescription = source.OrderDescription,
                OrderNotes = source.OrderNotes,
                OrderDate = source.OrderDate,
                Attachment = source.Attachment,
                CustomerId = source.CustomerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                OrderStatus = source.OrderStatus,
            };
            return caseType;
        }

        public static Models.Order CreateFromServerToClientLv(this Order source)
        {
            Models.Order order;
            if (source.Customer.Quotations.Any(x => x.OrderId == source.OrderId))
            {
                order = new Models.Order
                {
                    OrderId = source.OrderId,
                    OrderNo = source.OrderNo,
                    OrderDescription = source.OrderDescription,
                    OrderNotes = source.OrderNotes,
                    OrderDate = source.OrderDate,
                    Attachment = source.Attachment,
                    CustomerId = source.CustomerId,
                    RecCreatedBy = source.RecCreatedBy,
                    RecCreatedDt = source.RecCreatedDt,
                    RecLastUpdatedBy = source.RecLastUpdatedBy,
                    RecLastUpdatedDt = source.RecLastUpdatedDt,
                    OrderStatus = source.OrderStatus,
                    CustomerNameE = source.Customer.CreateFromServerToClient().CustomerNameE,
                    CustomerNameA = source.Customer.CreateFromServerToClient().CustomerNameA,
                    QuotationId = source.Customer.Quotations.FirstOrDefault(x => x.OrderId == source.OrderId).CreateFromServerToClient().QuotationId,
                };
                //order.Link = "<a href='CMS/Quotation/Create/" + order.QuotationId + "'>" + "Quotation " + order.QuotationId + "</a>";
            }
            else
            {
                order = new Models.Order
                {
                    OrderId = source.OrderId,
                    OrderNo = source.OrderNo,
                    OrderDescription = source.OrderDescription,
                    OrderNotes = source.OrderNotes,
                    OrderDate = source.OrderDate,
                    Attachment = source.Attachment,
                    CustomerId = source.CustomerId,
                    RecCreatedBy = source.RecCreatedBy,
                    RecCreatedDt = source.RecCreatedDt,
                    RecLastUpdatedBy = source.RecLastUpdatedBy,
                    RecLastUpdatedDt = source.RecLastUpdatedDt,
                    OrderStatus = source.OrderStatus,
                    CustomerNameE = source.Customer.CreateFromServerToClient().CustomerNameE,
                    CustomerNameA = source.Customer.CreateFromServerToClient().CustomerNameA,
                };
            }
            return order;
        }
        public static Models.Order CreateFromServerToClient(this Order source)
        {
            Models.Order order = new Models.Order
            {
                OrderId = source.OrderId,
                OrderNo = source.OrderNo,
                OrderDescription = source.OrderDescription,
                OrderNotes = source.OrderNotes,
                OrderDate = source.OrderDate,
                Attachment = source.Attachment,
                CustomerId = source.CustomerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                OrderStatus = source.OrderStatus,
                CustomerNameE = source.Customer.CreateFromServerToClient().CustomerNameE,
                CustomerNameA = source.Customer.CreateFromServerToClient().CustomerNameA,
            };
            return order;
        }

        public static DashboardModels.Order CreateForDashboard(this Order source)
        {
            return new DashboardModels.Order
            {
                OrderId = source.OrderId,
                OrderNo = source.OrderNo,
                OrderDate = Convert.ToDateTime(source.OrderDate).ToShortDateString(),
                OrderStatus = source.OrderStatus
            };
        }
    }
}