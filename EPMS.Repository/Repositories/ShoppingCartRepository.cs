using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingCartRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ShoppingCart> DbSet
        {
            get { return db.ShoppingCarts; }
        }

        #endregion

        #region Public

        public IEnumerable<ShoppingCart> GetCartByUserCartId(string userCartId)
        {
            return DbSet.Where(x => x.UserCartId == userCartId);
        }

        public ShoppingCart FindByUserCartId(string userCartId)
        {
            return DbSet.FirstOrDefault(x => x.UserCartId == userCartId && x.Status == 1);
        }

        #endregion
    }
}
