using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.Orders
{
    public class OrdersListViewModel
    {
        public OrdersListViewModel()
        {
            SearchRequest = new OrdersSearchRequest();
        }

        public IEnumerable<WebsiteModels.Order> aaData { get; set; }
        public OrdersSearchRequest SearchRequest { get; set; }
        
        public int iTotalRecords;
        public int iTotalDisplayRecords;
    }
}