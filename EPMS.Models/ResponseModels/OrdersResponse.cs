using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class OrdersResponse
    {
        public OrdersResponse()
        {
            Orders = new List<Order>();
            Order = new Order();
        }

        public IEnumerable<Order> Orders { get; set; }
        public Order Order { get; set; }

        /// <summary>
        /// Total Records
        /// </summary>
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}
