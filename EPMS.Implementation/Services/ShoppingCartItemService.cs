using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        #region Private

        private readonly IShoppingCartItemRepository itemRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingCartItemService(IShoppingCartItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        #endregion

        #region Public
        public ShoppingCartItem FindById(long id)
        {
            return itemRepository.Find(id);
        }

        public IEnumerable<ShoppingCartItem> GetAll()
        {
            return itemRepository.GetAll();
        }

        public bool AddShoppingCartItem(ShoppingCartItem cartItem)
        {
            try
            {
                itemRepository.Add(cartItem);
                itemRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateShoppingCartItem(ShoppingCartItem cartItem)
        {
            try
            {
                itemRepository.Update(cartItem);
                itemRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteShoppingCartItem(long cartItemId)
        {
            var cartItem = itemRepository.Find(cartItemId);
            if (cartItem != null)
            {
                itemRepository.Delete(cartItem);
                itemRepository.SaveChanges();
            }
        }

        #endregion
    }
}
