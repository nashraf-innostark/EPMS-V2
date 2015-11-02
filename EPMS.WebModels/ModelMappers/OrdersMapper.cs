using System;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class OrdersMapper
    {
        public static Order CreateFromClientToServer(this WebsiteModels.Order source)
        {
            string decspE = "";
            string decspA = "";
            if (!String.IsNullOrEmpty(source.OrderDescription))
            {
                decspE = source.OrderDescription.Replace("\n", "");
                decspE = decspE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.OrderNotes))
            {
                decspA = source.OrderNotes.Replace("\n", "");
                decspA = decspA.Replace("\r", "");
            }
            var caseType = new Order
            {
                OrderId = source.OrderId,
                OrderNo = source.OrderNo,
                OrderDescription = decspE,
                OrderNotes = decspA,
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

        public static WebsiteModels.Order CreateFromServerToClientLv(this Order source)
        {
            string decspE = "";
            string decspA = "";
            if (!String.IsNullOrEmpty(source.OrderDescription))
            {
                decspE = source.OrderDescription.Replace("\n", "");
                decspE = decspE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.OrderNotes))
            {
                decspA = source.OrderNotes.Replace("\n", "");
                decspA = decspA.Replace("\r", "");
            }
            WebsiteModels.Order order;
            if (source.Customer.Quotations.Any(x => x.OrderId == source.OrderId))
            {
                order = new WebsiteModels.Order
                {
                    OrderId = source.OrderId,
                    OrderNo = source.OrderNo,
                    OrderDescription = decspE,
                    OrderNotes = decspA,
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
                order = new WebsiteModels.Order
                {
                    OrderId = source.OrderId,
                    OrderNo = source.OrderNo,
                    OrderDescription = decspE,
                    OrderNotes = decspA,
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
        public static WebsiteModels.Order CreateFromServerToClient(this Order source)
        {
            string decspE = "";
            string decspA = "";
            if (!String.IsNullOrEmpty(source.OrderDescription))
            {
                decspE = source.OrderDescription.Replace("\n", "");
                decspE = decspE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.OrderNotes))
            {
                decspA = source.OrderNotes.Replace("\n", "");
                decspA = decspA.Replace("\r", "");
            }
            WebsiteModels.Order order = new WebsiteModels.Order
            {
                OrderId = source.OrderId,
                OrderNo = source.OrderNo,
                OrderDescription = decspE,
                OrderNotes = decspA,
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

        public static Models.DashboardModels.Order CreateForDashboard(this Order source)
        {
            return new Models.DashboardModels.Order
            {
                OrderId = source.OrderId,
                OrderNo = source.OrderNo,
                OrderDate = source.OrderDate != null ? Convert.ToDateTime(source.OrderDate.ToString()).ToShortDateString() : string.Empty,
                OrderStatus = source.OrderStatus,
                CustomerId = source.CustomerId
            };
        }
    }
}