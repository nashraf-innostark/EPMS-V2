﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using Microsoft.Practices.Unity;
using EPMS.Repository.BaseRepository;

namespace EPMS.Repository.Repositories
{
    class OrdersRepository : BaseRepository<Order>, IOrdersRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrdersRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Order> DbSet
        {
            get { return db.Orders; }
        }

        #endregion

        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<OrdersByColumn, Func<Order, object>> orderClause =

            new Dictionary<OrdersByColumn, Func<Order, object>>
                    {
                        { OrdersByColumn.OrderNumber,  c => c.OrderNo},
                        { OrdersByColumn.ClientName,  c => c.Customer.CustomerNameE},
                    };
        #endregion

        #region Public

        public OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;
            
            Expression<Func<Order, bool>> query =
                s => ((searchRequest.CustomerId == 0 || s.Customer.CustomerId == searchRequest.CustomerId) && (string.IsNullOrEmpty(searchRequest.SearchString)) || 
                    (s.OrderNo.Contains(searchRequest.SearchString)) || (s.Customer.CustomerNameE.Contains(searchRequest.SearchString)) ||
                    (s.Customer.CustomerNameA.Contains(searchRequest.SearchString)));

            IEnumerable<Order> orders = searchRequest.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(orderClause[searchRequest.OrdersByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(orderClause[searchRequest.OrdersByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new OrdersResponse { Orders = orders, TotalDisplayRecords = DbSet.Count(query), TotalRecords = DbSet.Count(query) };
        }

        public IEnumerable<Order> GetOrdersByCustomerId(long customerId)
        {
            var orders = DbSet.Where(order => order.CustomerId == customerId);
            return orders;
        }

        public Order GetOrderByOrderId(long orderId)
        {
            return DbSet.FirstOrDefault(order => order.OrderId == orderId);
        }

        #endregion
    }
}
