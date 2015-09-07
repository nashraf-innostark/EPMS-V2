using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.RequestModels
{
    public class ShoppingCartSearchRequest
    {
        public string UserCartId { get; set; }
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}