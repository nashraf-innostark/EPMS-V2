using System;
using System.Globalization;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class OrdersMapper
    {
        public static Order CreateFromClientToServer(this WebsiteModels.Order source)
        {
            string decsp = "";
            string notes = "";
            if (!String.IsNullOrEmpty(source.OrderDescription))
            {
                decsp = source.OrderDescription.Replace("\n", "");
                decsp = decsp.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.OrderNotes))
            {
                notes = source.OrderNotes.Replace("\n", "");
                notes = notes.Replace("\r", "");
            }
            var caseType = new Order
            {
                OrderId = source.OrderId,
                OrderNo = source.OrderNo,
                OrderDescription = decsp,
                OrderNotes = notes,
                CustomerId = source.CustomerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                OrderStatus = source.OrderStatus,
                QuotationId = source.QuotationId
            };
            return caseType;
        }

        public static WebsiteModels.Order CreateFromServerToClientLv(this Order source)
        {
            var order = new WebsiteModels.Order
            {
                OrderId = source.OrderId,
                OrderNo = source.OrderNo,
                OrderStatus = source.OrderStatus,
                CustomerId = source.CustomerId,
                CustomerNameE = source.Customer.CustomerNameE,
                CustomerNameA = source.Customer.CustomerNameA,
                QuotationId = source.QuotationId,
                FromOrder = source.Quotation.FromOrder
            };
            order.QuotationNumber = source.Quotation != null ? source.Quotation.SerialNumber : "";
            if (source.Quotation.Invoices.Any())
            {
                var invoice = source.Quotation.Invoices.FirstOrDefault();
                if (invoice != null)
                {
                    order.InvoiceId = invoice.InvoiceId;
                    order.InvoiceNumber = invoice.InvoiceNumber;
                    if (invoice.Receipts.Any())
                    {
                        order.Receipts = invoice.Receipts.Select(x => x.CreateFromServerToClient()).ToList();
                    }
                }
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
                CustomerId = source.CustomerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
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
                OrderDate = Convert.ToDateTime(source.RecCreatedDate.ToString(CultureInfo.CurrentCulture)).ToShortDateString(),
                OrderStatus = source.OrderStatus,
                CustomerId = source.CustomerId
            };
        }
    }
}