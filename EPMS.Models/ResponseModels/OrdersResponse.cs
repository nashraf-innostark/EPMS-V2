using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class OrdersResponse
    {
        public OrdersResponse()
        {
            Orders = new List<Order>();
        }

        public IEnumerable<Order> Orders { get; set; }

        /// <summary>
        /// Total Records
        /// </summary>
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}
