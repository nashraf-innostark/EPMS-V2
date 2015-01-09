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
            };
            return caseType;
        }

        public static Models.Order CreateFromServerToClient(this Order source)
        {
            return new Models.Order
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
                Customer = source.Customer.CreateFrom()
            };
        }
    }
}