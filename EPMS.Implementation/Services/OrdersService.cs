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
        private readonly IOrdersRepository OrdersRepository;
        
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public OrdersService(IOrdersRepository repository)
        {
            OrdersRepository = repository;
        }

        #endregion

        #region Public

        public bool AddOrder(Order order)
        {
            try
            {
                OrdersRepository.Add(order);
                OrdersRepository.SaveChanges();
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
                OrdersRepository.Update(order);
                OrdersRepository.SaveChanges();
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
                OrdersRepository.Delete(order);
                OrdersRepository.SaveChanges();
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        public OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest)
        {
            return OrdersRepository.GetAllOrders(searchRequest);
        }

        public IEnumerable<Order> GetOrdersByCustomerId(long customerId)
        {
            return OrdersRepository.GetOrdersByCustomerId(customerId);
        }

        public Order GetOrderByOrderId(long orderId)
        {
            return OrdersRepository.GetOrderByOrderId(orderId);
        }

        public OrdersLVResponse GetOrderForListView(OrdersSearchRequest searchRequest)
        {
            OrdersLVResponse response = new OrdersLVResponse();
            response.Order = OrdersRepository.GetAllOrders(searchRequest);
            return response;
        }

        #endregion
    }
}
