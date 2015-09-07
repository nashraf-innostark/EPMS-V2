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
        private readonly IShoppingCartItemRepository itemRepository;
        
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingCartService(IShoppingCartRepository repository, IShoppingCartItemRepository itemRepository)
        {
            this.repository = repository;
            this.itemRepository = itemRepository;
        }

        #endregion

        #region Public
        public ShoppingCart FindById(long id)
        {
            return repository.Find(id);
        }

        public ShoppingCart FindByUserCartId(string userCartId)
        {
            return repository.FindByUserCartId(userCartId);
        }

        public ShoppingCartResponse GetUserCart(string userCartId)
        {
            ShoppingCartResponse response =  new ShoppingCartResponse
            {
                ShoppingCarts = repository.GetCartByUserCartId(userCartId).ToList()
            };
            return response;
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

        public bool AddItemToCart(ShoppingCart cart)
        {
            try
            {
                repository.Add(cart);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SyncShoppingCart(ShoppingCartSearchRequest request)
        {
            var dbCartList = FindByUserCartId(request.UserCartId);
            if (dbCartList != null)
            {
                foreach (var shoppingCart in request.ShoppingCart.ShoppingCartItems)
                {
                    var cartItem = shoppingCart;
                    ShoppingCartItem listItem =
                        dbCartList.ShoppingCartItems.FirstOrDefault(
                            x => x.ProductId == cartItem.ProductId && x.SizeId == cartItem.SizeId);
                    if (listItem != null)
                    {
                        listItem.Quantity += shoppingCart.Quantity;
                        itemRepository.Update(listItem);
                    }
                    else
                    {
                        shoppingCart.CartId = dbCartList.CartId;
                        itemRepository.Add(shoppingCart);
                    }
                }
                itemRepository.SaveChanges();
            }
            else
            {
                repository.Add(request.ShoppingCart);
                repository.SaveChanges();
            }
            return true;
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

        public void DeleteShoppingCart(long cartId)
        {
            var cart = FindById(cartId);
            repository.Delete(cart);
            repository.SaveChanges();
        }
        #endregion
    }
}
