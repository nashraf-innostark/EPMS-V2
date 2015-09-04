using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IShoppingCartService
    {
        ShoppingCart FindById(long id);
        ShoppingCartResponse GetUserCart(string userCartId);
        bool AddToUserCart(ShoppingCartSearchRequest request);
        IEnumerable<ShoppingCart> GetAll();
        bool AddShoppingCart(IEnumerable<ShoppingCart> cart);
        bool UpdateShoppingCart(ShoppingCart cart);
        void DeleteShoppingCart(ShoppingCart cart);
    }
}
