using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Implementation.Services
{
    public class InventoryItemService : IInventoryItemService
    {
        private readonly IInventoryItemRepository inventoryItemRepository;
        private readonly IPoItemRepository poItemRepository;

        public InventoryItemService(IInventoryItemRepository inventoryItemRepository, IPoItemRepository poItemRepository)
        {
            this.inventoryItemRepository = inventoryItemRepository;
            this.poItemRepository = poItemRepository;
        }

        public IEnumerable<InventoryItem> GetAll()
        {
            return inventoryItemRepository.GetAll();
        }

        public InventoryItem FindItemById(long id)
        {
            var inventoryItem =  inventoryItemRepository.Find(id);
            CalculateQtyInHand(inventoryItem);
            return inventoryItem;
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

        public InventoryItemResponse GetAllInventoryItems(InventoryItemSearchRequest searchRequest)
        {
            return inventoryItemRepository.GetAllInventoryItems(searchRequest);
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
            var dbData = inventoryItemRepository.Find(inventoryItem.ItemId);
            if (dbData != null)
            {
                var itemToUpdate = dbData.UpdateDbDataFromClient(inventoryItem);
                inventoryItemRepository.Update(itemToUpdate);
                inventoryItemRepository.SaveChanges();
            }
        }

        private bool CheckAssociation(InventoryItem item)
        {
            if (item.ItemVariations.ToList().Any())
            {
                return true;
            }
            return false;
        }

        public void CalculateQtyInHand(InventoryItem item)
        {
            foreach (ItemVariation itemVariation in item.ItemVariations)
            {
                var poItems = poItemRepository.GetPoItemsByVarId(itemVariation.ItemVariationId);
                var manufacturerGroup = poItems.GroupBy(x => x.VendorId);

                foreach (var poItem in manufacturerGroup)
                {
                    var record = poItem.OrderByDescending(x => x.RecCreatedDate).FirstOrDefault();
                    if (itemVariation.ItemManufacturers.All(x => x.ManufacturerId != record.VendorId))
                    {
                        if (poItem.FirstOrDefault() != null)
                        {
                            var newManufacturer = new ItemManufacturer
                            {
                                Quantity = record.ItemQty,
                                Price = record.UnitPrice.ToString(),
                                ManufacturerId = (long)record.VendorId,
                                ItemVariationId = (long)record.ItemVariationId,
                                ManuallyAdded = true
                            };
                            itemVariation.ItemManufacturers.Add(newManufacturer);
                        }
                    }
                }

                if (poItems != null)
                    foreach (ItemManufacturer itemManufacturer in itemVariation.ItemManufacturers)
                    {
                        int oldQty = 0;
                        if (!itemManufacturer.ManuallyAdded)
                        {
                            oldQty = (int)itemManufacturer.Quantity;
                        }
                        var record =
                            poItems.OrderByDescending(x => x.RecCreatedDate).FirstOrDefault(x => x.VendorId == itemManufacturer.ManufacturerId);
                        if (record != null)
                        {
                            itemManufacturer.Price = record.UnitPrice.ToString();
                            itemManufacturer.Quantity = record.ItemQty;
                        }
                        itemManufacturer.TotalQuantity =
                            (int)poItems.Where(x => x.VendorId == itemManufacturer.ManufacturerId)
                                .Sum(x => x.ItemQty) + oldQty;
                    }

            }
        }


    }
}
