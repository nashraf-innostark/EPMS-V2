using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ShoppingCartResponse
    {
        public IList<ShoppingCart> ShoppingCarts { get; set; }
    }
}
