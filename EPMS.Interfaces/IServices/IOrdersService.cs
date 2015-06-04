using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IOrdersService
    {
        OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest);
        IEnumerable<Order> GetRecentOrders(string requester, int status);
        IEnumerable<Order> GetOrdersByCustomerId(long customerId);
        IEnumerable<Order> GetOrdersByCustomerIdWithRfis(long customerId);
        Order GetOrderByOrderId(long orderId);
        Order GetOrderByOrderNumber(string orderNo);
        OrdersLVResponse GetOrderForListView(OrdersSearchRequest searchRequest);
        IEnumerable<Order> GetAll();
        IEnumerable<AvailableOrdersDDL> GetAllAvailableOrdersDDL(long customerId);
        bool AddOrder(Order order);
        bool UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
