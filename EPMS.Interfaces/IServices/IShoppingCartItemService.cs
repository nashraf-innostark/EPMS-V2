using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IShoppingCartItemService
    {
        ShoppingCartItem FindById(long id);
        IEnumerable<ShoppingCartItem> GetAll();
        bool AddShoppingCartItem(ShoppingCartItem cartItem);
        bool UpdateShoppingCartItem(ShoppingCartItem cartItem);
        void DeleteShoppingCartItem(long cartItemId);
    }
}
