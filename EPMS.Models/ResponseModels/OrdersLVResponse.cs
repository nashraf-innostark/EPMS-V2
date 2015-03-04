using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class OrdersLVResponse
    {
        public OrdersResponse Order { get; set; }
        public Customer Customer { get; set; }
    }
}
