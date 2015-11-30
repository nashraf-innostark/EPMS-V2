using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IOrdersRepository : IBaseRepository<Order, int>
    {
        OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest);
        IEnumerable<Order> GetRecentOrders(string requester, int status);
        IEnumerable<Order> GetOrdersByCustomerId(long customerId);
        IEnumerable<Order> GetOrdersByCustomerId(QOReportCreateOrDetailsRequest request);
        IEnumerable<Order> GetOrdersByCustomerIdWithRfis(long customerId);
        Order GetOrderByOrderId(long orderId);
        Order GetOrderByOrderNumber(string orderNo);
        IEnumerable<Order> GetAllAvailableOrdersDDL(long customerId);
    }
}
