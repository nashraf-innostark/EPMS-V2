using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Orders
{
    public class OrdersCreateViewModel
    {
        public OrdersCreateViewModel()
        {
            Orders = new Order();
        }
        public Order Orders { get; set; }
        public string PageTitle { get; set; }
        public string BtnText { get; set; }

    }
}