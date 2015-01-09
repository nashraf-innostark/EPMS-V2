﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IOrdersService
    {
        OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest);
        IEnumerable<Order> GetOrdersByCustomerId(long customerId);
    }
}
