using System;
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
                        //{ OrdersByColumn.Quotation,  c => c.Customer.Quotations.FirstOrDefault(x => x.OrderId == c.OrderId) != null ? c.Customer.Quotations.FirstOrDefault(x => x.OrderId == c.OrderId).QuotationDiscount : 0 },
                        //{ OrdersByColumn.Invoice,  c => c.},
                        //{ OrdersByColumn.Reciepts,  c => c.},
                        { OrdersByColumn.Status,  c => c.OrderStatus},
                    };
        #endregion

        #region Public

        public OrdersResponse GetAllOrders(OrdersSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;
            
            Expression<Func<Order, bool>> query =
                s => ((searchRequest.CustomerId == 0 || (s.Customer.CustomerId == searchRequest.CustomerId)) && ((string.IsNullOrEmpty(searchRequest.SearchString)) || 
                    (s.OrderNo.Contains(searchRequest.SearchString)) || (s.Customer.CustomerNameE.Contains(searchRequest.SearchString)) ||
                    (s.Customer.CustomerNameA.Contains(searchRequest.SearchString))));
            
            IEnumerable<Order> orders = searchRequest.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(orderClause[searchRequest.OrdersByColumn])
                .Skip(fromRow).Take(toRow).Select(s => new Order
                {
                    OrderId = s.OrderId,
                    OrderNo = s.OrderNo,
                    OrderStatus = s.OrderStatus,
                    QuotationId = s.QuotationId,
                    Quotation = new Quotation
                    {
                        SerialNumber = s.Quotation != null ? s.Quotation.SerialNumber : "",
                        FromOrder = s.Quotation != null && s.Quotation.FromOrder,
                        Invoices = s.Quotation != null ? s.Quotation.Invoices.Where(x=>x.QuotationId == s.QuotationId).ToList() : new List<Invoice>()
                    },
                    CustomerId = s.CustomerId,
                    Customer = s.Customer
                }).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(orderClause[searchRequest.OrdersByColumn]).Skip(fromRow).Take(toRow).Select(s => new Order
                                           {
                                               OrderId = s.OrderId,
                                               OrderNo = s.OrderNo,
                                               OrderStatus = s.OrderStatus,
                                               QuotationId = s.QuotationId,
                                               Quotation = new Quotation
                                               {
                                                   SerialNumber = s.Quotation.SerialNumber,
                                                   Invoices = s.Quotation != null ? s.Quotation.Invoices.Where(x => x.QuotationId == s.QuotationId).ToList() : new List<Invoice>()
                                               },
                                               CustomerId = s.CustomerId,
                                               Customer = s.Customer
                                           }).ToList();
            return new OrdersResponse { Orders = orders, TotalDisplayRecords = DbSet.Count(query), TotalRecords = DbSet.Count(query) };
        }

        public IEnumerable<Order> GetOrdersByCustomerId(long customerId)
        {
            var orders = DbSet.Where(order => order.CustomerId == customerId);
            return orders;
        }

        public IEnumerable<Order> GetOrdersByCustomerIdWithRfis(long customerId)
        {
            var orders = DbSet.Where(order => order.CustomerId == customerId).Include(x=>x.RFIs);
            return orders;
        }

        public IEnumerable<Order> GetRecentOrders(string requester, int status)
        {
            if (requester == "Admin")
            {
                if(status>0)
                    return DbSet.Where(x => x.OrderStatus == status).OrderByDescending(x => x.RecCreatedDate).Take(5);
                return DbSet.OrderByDescending(x => x.RecCreatedDate).Take(5);
            }
            long customerId = Convert.ToInt64(requester);
            if (status > 0)
                return DbSet.Where(x => x.CustomerId == customerId && x.OrderStatus == status).OrderByDescending(x => x.RecCreatedDate).Take(5);
            return DbSet.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.RecCreatedDate).Take(5);
        }

        public Order GetOrderByOrderId(long orderId)
        {
            return DbSet.FirstOrDefault(order => order.OrderId == orderId);
        }

        public Order GetOrderByOrderNumber(string orderNo)
        {
            return DbSet.FirstOrDefault(order => order.OrderNo == orderNo);
        }

        public IEnumerable<Order> GetAllAvailableOrdersDDL(long customerId)
        {
            return DbSet.Where(x => x.OrderStatus == (short) OrderStatus.Pending && x.CustomerId == customerId);
        }

        #endregion
    }
}
