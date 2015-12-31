using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.WebsiteClient
{
    public class ShoppingCartListViewModel
    {
        public IList<ShoppingCart> ShoppingCarts { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public double GrandTotal { get; set; }
        public bool ShowProductPrice { get; set; }
    }
}
