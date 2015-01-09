using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IOrdersService
    {
        OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest);
        IEnumerable<Order> GetOrdersByCustomerId(long customerId);
        Order GetOrderByOrderId(long orderId);
        OrdersLVResponse GetOrderForListView(OrdersSearchRequest searchRequest);
        bool AddOrder(Order order);
        bool UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
