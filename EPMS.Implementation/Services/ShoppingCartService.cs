using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        #region Private

        private readonly IShoppingCartRepository repository;
        
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingCartService(IShoppingCartRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Public
        public ShoppingCart FindById(long id)
        {
            return repository.Find(id);
        }

        public ShoppingCartResponse GetUserCart(string userCartId)
        {
            ShoppingCartResponse response =  new ShoppingCartResponse
            {
                ShoppingCarts = repository.GetCartByUserCartId(userCartId).ToList()
            };
            return response;
        }

        public bool AddToUserCart(ShoppingCartSearchRequest request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return repository.GetAll();
        }

        public bool AddShoppingCart(IEnumerable<ShoppingCart> cart)
        {
            try
            {
                foreach (var shoppingCart in cart)
                {
                    repository.Add(shoppingCart);
                    repository.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateShoppingCart(ShoppingCart cart)
        {
            try
            {
                repository.Update(cart);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteShoppingCart(ShoppingCart cart)
        {
            repository.Delete(cart);
            repository.SaveChanges();
        }
        #endregion
    }
}
