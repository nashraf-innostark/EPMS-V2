using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IShoppingCartRepository : IBaseRepository<ShoppingCart, long>
    {
        IEnumerable<ShoppingCart> GetCartByUserCartId(string userCartId);
    }
}
