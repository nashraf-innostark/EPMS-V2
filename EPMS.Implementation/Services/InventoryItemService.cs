using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Implementation.Services
{
    public class InventoryItemService : IInventoryItemService
    {
        private readonly IInventoryItemRepository inventoryItemRepository;

        public InventoryItemService(IInventoryItemRepository inventoryItemRepository)
        {
            this.inventoryItemRepository = inventoryItemRepository;
        }

        public IEnumerable<InventoryItem> GetAll()
        {
            return inventoryItemRepository.GetAll();
        }

        public InventoryItem FindItemById(long id)
        {
            return inventoryItemRepository.Find(id);
        }

        public bool AddItem(InventoryItem item)
        {
            if (inventoryItemRepository.ItemExists(item))
            {
                throw new ArgumentException("Item already exists");
            }
            inventoryItemRepository.Add(item);
            inventoryItemRepository.SaveChanges();
            return true;
        }

        public bool UpdateItem(InventoryItem item)
        {
            if (inventoryItemRepository.ItemExists(item))
            {
                throw new ArgumentException("Item already exists");
            }
            inventoryItemRepository.Update(item);
            inventoryItemRepository.SaveChanges();
            return true;
        }

        public string DeleteItem(long itemId)
        {
            InventoryItem item = inventoryItemRepository.Find(itemId);
            if (item == null)
            {
                return "Error";
            }
            if (CheckAssociation(item))
            {
                return "Associated";
            }
            inventoryItemRepository.Delete(item);
            inventoryItemRepository.SaveChanges();
            return "Success";
        }

        public SaveInventoryItemResponse SaveItem(InventoryItemRequest itemToSave)
        {
            #region Update

            if (itemToSave.InventoryItem.ItemId > 0)
            {
                UpdateInventoryItem(itemToSave.InventoryItem);
            }

            #endregion

            #region Add

            else
            {
                SaveInventoryItem(itemToSave.InventoryItem);
            }

            #endregion

            return new SaveInventoryItemResponse();
        }

        private void SaveInventoryItem(InventoryItem inventoryItem)
        {
            inventoryItem.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            inventoryItem.RecCreatedDt = DateTime.Now;
            inventoryItem.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            inventoryItem.RecLastUpdatedDt = DateTime.Now;
            inventoryItemRepository.Add(inventoryItem);
            inventoryItemRepository.SaveChanges();
        }

        private void UpdateInventoryItem(InventoryItem inventoryItem)
        {
            inventoryItem.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            inventoryItem.RecLastUpdatedDt = DateTime.Now;
            inventoryItemRepository.Update(inventoryItem);
            inventoryItemRepository.SaveChanges();
        }

        private bool CheckAssociation(InventoryItem item)
        {
            if (item.ItemVariations.ToList().Any())
            {
                return true;
            }
            return false;
        }
    }
}
