using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IShoppingCartItemRepository : IBaseRepository<ShoppingCartItem, long>
    {
        ShoppingCartItem FindByCartId(long cartId);
    }
}
