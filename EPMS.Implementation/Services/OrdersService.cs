using System;
using System.Collections.Generic;
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

        #region Public

        public bool AddOrder(Order order)
        {
            try
            {
                Repository.Add(order);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateOrder(Order order)
        {
            try
            {
                Repository.Update(order);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                Repository.Delete(order);
                Repository.SaveChanges();
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        public OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest)
        {
            return Repository.GetAllOrders(searchRequest);
        }

        public IEnumerable<Order> GetOrdersByCustomerId(long customerId)
        {
            return Repository.GetOrdersByCustomerId(customerId);
        }

        public Order GetOrderByOrderId(long orderId)
        {
            return Repository.GetOrderByOrderId(orderId);
        }

        #endregion
    }
}
