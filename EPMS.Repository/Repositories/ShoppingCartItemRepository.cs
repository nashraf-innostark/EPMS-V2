using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ShoppingCartItemRepository : BaseRepository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingCartItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ShoppingCartItem> DbSet
        {
            get { return db.ShoppingCartItems; }
        }

        #endregion

        #region Public

        public ShoppingCartItem FindByCartId(long cartId)
        {
            return DbSet.FirstOrDefault(x => x.CartId == cartId);
        }

        #endregion
    }
}
