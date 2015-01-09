﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository Repository;
        
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public OrdersService(IOrdersRepository repository)
        {
            Repository = repository;
        }

        #endregion
        public OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest)
        {
            return Repository.GetAllOrders(searchRequest);
        }

        public IEnumerable<Order> GetOrdersByCustomerId(long customerId)
        {
            return Repository.GetOrdersByCustomerId(customerId);
        }
    }
}
