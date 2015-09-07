using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IShoppingCartService
    {
        ShoppingCart FindById(long id);
        ShoppingCart FindByUserCartId(string userCartId);
        ShoppingCartResponse GetUserCart(string userCartId);
        IEnumerable<ShoppingCart> GetAll();
        // Add multiple item to user cart
        bool AddShoppingCart(IEnumerable<ShoppingCart> cart);
        // Add single item to user cart
        bool AddItemToCart(ShoppingCart cart);
        // Sync cart
        bool SyncShoppingCart(ShoppingCartSearchRequest request);
        bool UpdateShoppingCart(ShoppingCart cart);
        void DeleteShoppingCart(long cartId);
    }
}
