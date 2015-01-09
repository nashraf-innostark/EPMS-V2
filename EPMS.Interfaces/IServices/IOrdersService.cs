using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IOrdersService
    {
        OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest);
    }
}
