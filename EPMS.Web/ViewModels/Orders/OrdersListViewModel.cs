using System.Collections.Generic;
using EPMS.Models.RequestModels;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Orders
{
    public class OrdersListViewModel
    {
        public OrdersListViewModel()
        {
            SearchRequest = new OrdersSearchRequest();
        }
        public IEnumerable<Order> aaData { get; set; }
        public OrdersSearchRequest SearchRequest { get; set; }
        
        public int iTotalRecords;
        public int iTotalDisplayRecords;
    }
}