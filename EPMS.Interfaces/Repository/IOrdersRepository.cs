using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IOrdersRepository : IBaseRepository<Order, int>
    {
        OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest);
        IEnumerable<Order> GetOrdersByCustomerId(long customerId);
        Order GetOrderByOrderId(long orderId);
    }
}
