using System;
using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class OrdersSearchRequest : GetPagedListRequest
    {
        public Guid UserId { get; set; }
        public long CustomerId { get; set; }
        public string Role { get; set; }

        public OrdersByColumn OrdersByColumn
        {
            get
            {
                return (OrdersByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
