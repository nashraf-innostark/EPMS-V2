using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.WebsiteClient
{
    public class ShoppingCartListViewModel
    {
        public IList<ShoppingCart> ShoppingCarts { get; set; }
        public double GrandTotal { get; set; }
    }
}
