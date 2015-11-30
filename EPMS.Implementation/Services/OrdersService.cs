using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository ordersRepository;
        
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public OrdersService(IOrdersRepository repository)
        {
            ordersRepository = repository;
        }

        #endregion

        #region Public

        public IEnumerable<AvailableOrdersDDL> GetAllAvailableOrdersDDL(long customerId)
        {
            var ordersDDL = ordersRepository.GetAllAvailableOrdersDDL(customerId);
            return ordersDDL.Select(x => x.CreateForDDL());
        }

        public bool AddOrder(Order order)
        {
            try
            {
                ordersRepository.Add(order);
                ordersRepository.SaveChanges();
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
                ordersRepository.Update(order);
                ordersRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteOrder(Order order)
        {
            ordersRepository.Delete(order);
            ordersRepository.SaveChanges();
        }
        public OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest)
        {
            return ordersRepository.GetAllOrders(searchRequest);
        }

        public OrdersResponse GetOrderResponse(int orderId)
        {
            OrdersResponse response = new OrdersResponse
            {
                Orders = ordersRepository.GetAll().OrderBy(x=>x.RecCreatedDate).ToList()
            };
            if (orderId > 0)
            {
                response.Order = ordersRepository.Find(orderId);
            }
            return response;
        }

        public IEnumerable<Order> GetRecentOrders(string requester, int status)
        {
            return ordersRepository.GetRecentOrders(requester, status);
        }

        public IEnumerable<Order> GetOrdersByCustomerId(long customerId)
        {
            return ordersRepository.GetOrdersByCustomerId(customerId);
        }

        public IEnumerable<Order> GetOrdersByCustomerIdWithRfis(long customerId)
        {
            return ordersRepository.GetOrdersByCustomerId(customerId);
        }

        public Order GetOrderByOrderId(long orderId)
        {
            return ordersRepository.GetOrderByOrderId(orderId);
        }

        public Order GetOrderByOrderNumber(string orderNo)
        {
            return ordersRepository.GetOrderByOrderNumber(orderNo);
        }

        public OrdersLVResponse GetOrderForListView(OrdersSearchRequest searchRequest)
        {
            OrdersLVResponse response = new OrdersLVResponse();
            response.Order = ordersRepository.GetAllOrders(searchRequest);
            return response;
        }

        public IEnumerable<Order> GetAll()
        {
            return ordersRepository.GetAll();
        }

        #endregion
    }
}
