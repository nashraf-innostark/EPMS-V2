using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;
using EPMS.Repository.Repositories;
using Microsoft.AspNet.Identity;

namespace EPMS.Implementation.Services
{
    class ItemVariationService : IItemVariationService
    {
        #region Private
        private readonly IItemVariationRepository variationRepository;
        private readonly ISizeRepository sizeRepository;
        private readonly IManufacturerRepository manufacturerRepository;
        private readonly IStatusRepository statusRepository;
        private readonly IColorRepository colorRepository;
        private readonly IItemImageRepository imageRepository;
        private readonly IItemManufacturerRepository itemManufacturerRepository;
        private readonly IItemWarehouseRepository itemWarehouseRepository;
        private readonly INotificationService notificationService;
        private readonly IWarehouseService warehouseService;
        private readonly IInventoryItemRepository inventoryItemRepository;
        private readonly ItemWarehouseService itemWarehouseService;
        private readonly ItemReleaseQuantityRepository itemReleaseQuantityRepository;

        #endregion

        #region Constructor

        public ItemVariationService(IItemVariationRepository variationRepository, ISizeRepository sizeRepository,
            IManufacturerRepository manufacturerRepository, IStatusRepository statusRepository,
            IColorRepository colorRepository, IItemImageRepository imageRepository,
            IItemManufacturerRepository itemManufacturerRepository, IItemWarehouseRepository itemWarehouseRepository,
            INotificationService notificationService, IWarehouseService warehouseService, IInventoryItemRepository inventoryItemRepository, ItemWarehouseService itemWarehouseService, ItemReleaseQuantityRepository itemReleaseQuantityRepository)
        {
            this.variationRepository = variationRepository;
            this.sizeRepository = sizeRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.statusRepository = statusRepository;
            this.colorRepository = colorRepository;
            this.imageRepository = imageRepository;
            this.itemManufacturerRepository = itemManufacturerRepository;
            this.itemWarehouseRepository = itemWarehouseRepository;
            this.notificationService = notificationService;
            this.warehouseService = warehouseService;
            this.inventoryItemRepository = inventoryItemRepository;
            this.itemWarehouseService = itemWarehouseService;
            this.itemReleaseQuantityRepository = itemReleaseQuantityRepository;
        }

        #endregion

        #region Public
        public IEnumerable<ItemVariation> GetAll()
        {
            return variationRepository.GetAll();
        }

        public ItemVariation FindVariationById(long id)
        {
            return variationRepository.Find(id);
        }

        public bool AddVariation(ItemVariation itemVariation)
        {
            variationRepository.Add(itemVariation);
            variationRepository.SaveChanges();
            return true;
        }

        public bool UpdateVariation(ItemVariation itemVariation)
        {
            variationRepository.Update(itemVariation);
            variationRepository.SaveChanges();
            return true;
        }

        public void DeleteVartiation(ItemVariation itemVariation)
        {
            variationRepository.Delete(itemVariation);
            variationRepository.SaveChanges();
        }

        public IEnumerable<ItemVariationDropDownListItem> GetItemVariationDropDownList()
        {
            return variationRepository.GetItemVariationDropDownList();
        }

        public ItemVariationResponse ItemVariationResponse(long id, long itemVariationId)
        {
            ItemVariationResponse response = new ItemVariationResponse();
            response.ItemVariation = id > 0 ? variationRepository.Find(id) : new ItemVariation();
            response.ColorsForDdl = colorRepository.GetAll();
            response.SizesForDdl = sizeRepository.GetAll();
            response.ManufacturersForDdl = manufacturerRepository.GetAll();
            response.StatusesForDdl = statusRepository.GetAll();
            response.WarehousesForDdl = warehouseService.GetAll();
            response.InventoryItem = inventoryItemRepository.Find(itemVariationId);

            //response.ItemVariation.ItemWarehouses = new List<ItemWarehouse>();
            if (response.ItemVariation.ItemWarehouses != null)
            {
                var releaseQtyList = itemReleaseQuantityRepository.GetAll();
                foreach (ItemWarehouse itemWarehouse in response.ItemVariation.ItemWarehouses)
                {
                    var itemReleaseQuantity =
                        releaseQtyList.Where(
                            x =>
                                x.WarehouseId == itemWarehouse.WarehouseId &&
                                x.ItemVariationId == itemWarehouse.ItemVariationId).Sum(y => y.Quantity);
                    if (itemReleaseQuantity != null)
                    {
                        itemWarehouse.Quantity = itemWarehouse.Quantity - itemReleaseQuantity;
                    }
                }
            }
            return response;
        }

        public IEnumerable<ItemVariation> GetVariationsByInventoryItemId(long inventoryItemId)
        {
            return variationRepository.GetVariationsByInventoryItemId(inventoryItemId);
        }
        /// <summary>
        /// Save Item Variation from Client
        /// </summary>
        public ItemVariationResponse SaveItemVariation(ItemVariationRequest variationToSave)
        {
            ItemVariation itemVariationFromDatabase = variationRepository.Find(variationToSave.ItemVariation.ItemVariationId);
            if (variationToSave.ItemVariation.ItemVariationId > 0)
            {
                UpdateItemVariation(variationToSave.ItemVariation);
                UpdateSizeList(variationToSave, itemVariationFromDatabase);
                UpdateManufacturerList(variationToSave);
                UpdateWarehouseList(variationToSave);
                UpdateStatusList(variationToSave, itemVariationFromDatabase);
                UpdateColorList(variationToSave, itemVariationFromDatabase);
                UpdateImages(variationToSave, itemVariationFromDatabase);
            }
            else
            {
                AddNewVariation(variationToSave.ItemVariation);
                AddSizeList(variationToSave);
                AddManufacturerList(variationToSave);
                AddWarehouseList(variationToSave);
                AddStatusList(variationToSave);
                AddColorList(variationToSave);
                AddImages(variationToSave);
            }
            variationRepository.SaveChanges();
            return new ItemVariationResponse();
        }

        /// <summary>
        /// Add New Variation from Client
        /// </summary>
        private void AddNewVariation(ItemVariation itemVariation)
        {
            itemVariation.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            itemVariation.RecCreatedDt = DateTime.Now;
            itemVariation.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            itemVariation.RecLastUpdatedDt = DateTime.Now;
            variationRepository.Add(itemVariation);
            //Item variation Notification
            SendNotification(itemVariation);
        }

        /// <summary>
        /// Update Existing Variation from Client
        /// </summary>
        private void UpdateItemVariation(ItemVariation itemVariation)
        {
            itemVariation.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            itemVariation.RecLastUpdatedDt = DateTime.Now;
            variationRepository.Update(itemVariation);
        }
        /// <summary>
        /// Save Size List from Client
        /// </summary>
        private void AddSizeList(ItemVariationRequest variationToSave)
        {
            if (variationToSave.SizeArrayList != null)
            {
                string[] sizeList = variationToSave.SizeArrayList.Split(',');
                foreach (string item in sizeList)
                {
                    Size sizeToAdd = sizeRepository.Find(Convert.ToInt64(item));
                    if (sizeToAdd == null)
                        throw new Exception("Size not found in database");
                    if (variationToSave.ItemVariation.Sizes == null)
                    {
                        variationToSave.ItemVariation.Sizes = new Collection<Size>();
                    }
                    variationToSave.ItemVariation.Sizes.Add(sizeToAdd);
                }
            }
        }

        /// <summary>
        /// Update Size List from Client
        /// </summary>
        private void UpdateSizeList(ItemVariationRequest variationToSave, ItemVariation itemVariationFromDatabase)
        {
            List<Size> dbList = itemVariationFromDatabase.Sizes.ToList();

            //Add New Items from Clientlist to Database
            if (variationToSave.SizeArrayList != null)
            {
                string[] clientList = variationToSave.SizeArrayList.Split(',');
                var result = dbList.Where(p => clientList.All(p2 => Convert.ToInt64(p2) != p.SizeId));
                foreach (string item in clientList)
                {
                    Size sizeToAdd = sizeRepository.Find(Convert.ToInt64(item));
                    if (dbList.Any(a => a.SizeId == sizeToAdd.SizeId))
                        continue;
                    itemVariationFromDatabase.Sizes.Add(sizeToAdd);
                }

                //Remove Items from Database that are not in Clientlist
                foreach (Size size in result.ToList())
                {
                    itemVariationFromDatabase.Sizes.Remove(size);
                }
            }
            else
            {
                //Remove All Items from Database if Clientlist is Empty
                foreach (Size size in dbList)
                {
                    itemVariationFromDatabase.Sizes.Remove(size);
                }
            }
        }

        /// <summary>
        /// Add Warehouse List from Client
        /// </summary>
        /// <param name="variationToSave"></param>
        private void AddWarehouseList(ItemVariationRequest variationToSave)
        {
            if (variationToSave.ItemWarehouses != null)
            {
                foreach (ItemWarehouse itemWarehouse in variationToSave.ItemWarehouses)
                {
                    ItemWarehouse warehouse = new ItemWarehouse
                    {
                        ItemVariationId = variationToSave.ItemVariation.ItemVariationId,
                        WarehouseId = itemWarehouse.WarehouseId,
                        Quantity = itemWarehouse.Quantity,
                        PlaceInWarehouse = itemWarehouse.PlaceInWarehouse
                    };
                    itemWarehouseRepository.Add(warehouse);
                }
                itemWarehouseRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Update Warehouse List from Client
        /// </summary>
        private void UpdateWarehouseList(ItemVariationRequest variationToSave)
        {
            IEnumerable<ItemWarehouse> dbList =
                itemWarehouseRepository.GetItemsByVariationId(variationToSave.ItemVariation.ItemVariationId).ToList();
            IEnumerable<ItemWarehouse> clientList = variationToSave.ItemWarehouses;
            //If Client List contains Entries
            if (clientList != null && clientList.Any())
            {
                //Add New Items
                foreach (ItemWarehouse itemWarehouse in clientList)
                {
                    //Add New Items from Client list
                    if (dbList.Any(a => a.WarehouseId == itemWarehouse.WarehouseId))
                        continue;
                    ItemWarehouse itemToAdd = new ItemWarehouse
                    {
                        ItemVariationId = variationToSave.ItemVariation.ItemVariationId,
                        WarehouseId = itemWarehouse.WarehouseId,
                        Quantity = itemWarehouse.Quantity,
                        PlaceInWarehouse = itemWarehouse.PlaceInWarehouse
                    };
                    itemWarehouseRepository.Add(itemToAdd);

                    //Delete Items from DB List which are not in Client List
                    foreach (ItemWarehouse warehouseItem in dbList)
                    {
                        if (clientList.Any(x => x.WarehouseId == warehouseItem.WarehouseId))
                            continue;
                        var itemToDelete = itemWarehouseRepository.Find(warehouseItem.WarehouseId);
                        itemWarehouseRepository.Delete(itemToDelete);
                    }
                }
            }
            else
            {
                //Delete All Items if List from Client is Empty
                foreach (ItemWarehouse warehouseItem in dbList)
                {
                    var itemToDelete = itemWarehouseRepository.FindItemWarehouseByVariationAndManufacturerId(warehouseItem.ItemVariationId, warehouseItem.WarehouseId);
                    itemWarehouseRepository.Delete(itemToDelete);
                }
            }
            itemWarehouseRepository.SaveChanges();
        }

        /// <summary>
        /// Add Manufacturer List from Client
        /// </summary>
        /// <param name="variationToSave"></param>
        private void AddManufacturerList(ItemVariationRequest variationToSave)
        {
            if (variationToSave.ItemManufacturers != null)
            {
                foreach (ItemManufacturer itemManufacturer in variationToSave.ItemManufacturers)
                {
                    ItemManufacturer manufacturer = new ItemManufacturer
                    {
                        ItemVariationId = variationToSave.ItemVariation.ItemVariationId,
                        Price = itemManufacturer.Price,
                        ManufacturerId = itemManufacturer.ManufacturerId
                    };
                    itemManufacturerRepository.Add(manufacturer);
                }
                itemManufacturerRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Update Manufacturer List from Client
        /// </summary>
        private void UpdateManufacturerList(ItemVariationRequest variationToSave)
        {
            IEnumerable<ItemManufacturer> dbList =
                itemManufacturerRepository.GetItemsByVariationId(variationToSave.ItemVariation.ItemVariationId).ToList();
            IEnumerable<ItemManufacturer> clientList = variationToSave.ItemManufacturers;
            //If Client List contains Entries
            if (clientList != null && clientList.Any())
            {
                //Add New Items
                foreach (ItemManufacturer itemManufacturer in clientList)
                {
                    if (itemManufacturer.ManufacturerId > 0 && itemManufacturer.ItemVariationId > 0)
                    {
                        //Update Items
                        itemManufacturerRepository.Update(itemManufacturer);
                    }
                    else
                    {
                        //Add New Items from Client list
                        ItemManufacturer itemToAdd = new ItemManufacturer
                        {
                            ItemVariationId = variationToSave.ItemVariation.ItemVariationId,
                            ManufacturerId = itemManufacturer.ManufacturerId,
                            Price = itemManufacturer.Price,
                        };
                        itemManufacturerRepository.Add(itemToAdd);
                    }

                    //Delete Items from DB List which are not in Client List
                    foreach (ItemManufacturer manufacturerItem in dbList)
                    {
                        if (clientList.Any(x => x.ManufacturerId == manufacturerItem.ManufacturerId))
                            continue;
                        var itemToDelete = itemManufacturerRepository.Find(manufacturerItem.ManufacturerId);
                        itemManufacturerRepository.Delete(itemToDelete);
                    }
                }
            }
            else
            {
                //Delete All Items if List from Client is Empty
                foreach (ItemManufacturer manufacturerItem in dbList)
                {
                    var itemToDelete = itemManufacturerRepository.FindItemManufacturerByVariationAndManufacturerId(manufacturerItem.ItemVariationId, manufacturerItem.ManufacturerId);
                    itemManufacturerRepository.Delete(itemToDelete);
                }
            }
            itemManufacturerRepository.SaveChanges();
        }

        /// <summary>
        /// Add Status List from Client
        /// </summary>
        private void AddStatusList(ItemVariationRequest variationToSave)
        {
            if (variationToSave.StatusArrayList != null)
            {
                string[] statusList = variationToSave.StatusArrayList.Split(',');
                foreach (string item in statusList)
                {
                    Status statusToAdd = statusRepository.Find(Convert.ToInt64(item));
                    if (statusToAdd == null)
                        throw new Exception("Status not found in database");
                    if (variationToSave.ItemVariation.Status == null)
                    {
                        variationToSave.ItemVariation.Status = new Collection<Status>();
                    }
                    variationToSave.ItemVariation.Status.Add(statusToAdd);
                }
            }
        }

        /// <summary>
        /// Update Status List from Client
        /// </summary>
        private void UpdateStatusList(ItemVariationRequest variationToSave, ItemVariation itemVariationFromDatabase)
        {
            List<Status> dbList = itemVariationFromDatabase.Status.ToList();

            //Add New Items from Clientlist to Database
            if (variationToSave.StatusArrayList != null)
            {
                string[] clientList = variationToSave.StatusArrayList.Split(',');
                var result = dbList.Where(p => clientList.All(p2 => Convert.ToInt64(p2) != p.StatusId));
                foreach (string item in clientList)
                {
                    Status statusToAdd = statusRepository.Find(Convert.ToInt64(item));
                    if (dbList.Any(a => a.StatusId == statusToAdd.StatusId))
                        continue;
                    itemVariationFromDatabase.Status.Add(statusToAdd);
                }

                //Remove Items from Database that are not in Clientlist
                foreach (Status status in result.ToList())
                {
                    itemVariationFromDatabase.Status.Remove(status);
                }
            }
            else
            {
                //Remove All Items from Database if Clientlist is Empty
                foreach (Status status in dbList)
                {
                    itemVariationFromDatabase.Status.Remove(status);
                }
            }
        }

        /// <summary>
        /// Add Color List from Client
        /// </summary>
        /// <param name="variationToSave"></param>
        private void AddColorList(ItemVariationRequest variationToSave)
        {
            if (variationToSave.ColorArrayList != null)
            {
                string[] colorList = variationToSave.ColorArrayList.Split(',');
                foreach (string item in colorList)
                {
                    Color colorToAdd = colorRepository.Find(Convert.ToInt64(item));
                    if (colorToAdd == null)
                        throw new Exception("Status not found in database");
                    if (variationToSave.ItemVariation.Colors == null)
                    {
                        variationToSave.ItemVariation.Colors = new Collection<Color>();
                    }
                    variationToSave.ItemVariation.Colors.Add(colorToAdd);
                }
            }
        }

        /// <summary>
        /// Update Color list from Client
        /// </summary>
        private void UpdateColorList(ItemVariationRequest variationToSave, ItemVariation itemVariationFromDatabase)
        {
            List<Color> dbList = itemVariationFromDatabase.Colors.ToList();

            //Add New Items from Clientlist to Database
            if (variationToSave.ColorArrayList != null)
            {
                string[] clientList = variationToSave.ColorArrayList.Split(',');
                var result = dbList.Where(p => clientList.All(p2 => Convert.ToInt64(p2) != p.ColorId));
                foreach (string item in clientList)
                {
                    Color colorToAdd = colorRepository.Find(Convert.ToInt64(item));
                    if (dbList.Any(a => a.ColorId == colorToAdd.ColorId))
                        continue;
                    itemVariationFromDatabase.Colors.Add(colorToAdd);
                }

                //Remove Items from Database that are not in Clientlist
                foreach (Color color in result.ToList())
                {
                    itemVariationFromDatabase.Colors.Remove(color);
                }
            }
            else
            {
                //Remove All Items from Database if Clientlist is Empty
                foreach (Color color in dbList)
                {
                    itemVariationFromDatabase.Colors.Remove(color);
                }
            }
        }

        private void AddImages(ItemVariationRequest variationToSave)
        {
            if (variationToSave.ItemImages != null)
            {
                foreach (ItemImage itemImage in variationToSave.ItemImages)
                {
                    ItemImage image = new ItemImage
                    {
                        ItemVariationId = variationToSave.ItemVariation.ItemVariationId,
                        ImageOrder = itemImage.ImageOrder,
                        ItemImagePath = itemImage.ItemImagePath,
                        ShowImage = itemImage.ShowImage
                    };
                    imageRepository.Add(image);
                }
                imageRepository.SaveChanges();
            }
        }

        private void UpdateImages(ItemVariationRequest variationToSave, ItemVariation itemVariationFromDatabase)
        {
            IEnumerable<ItemImage> dbList = itemVariationFromDatabase.ItemImages.ToList();
            IEnumerable<ItemImage> clientList = variationToSave.ItemImages;

            if (clientList != null)
            {
                //Add New Items
                foreach (ItemImage image in clientList)
                {
                    if (image.ImageId > 0)
                    {
                        imageRepository.Update(image);
                    }
                    else
                    {
                        ItemImage itemImage = new ItemImage
                        {
                            ItemVariationId = variationToSave.ItemVariation.ItemVariationId,
                            ImageOrder = image.ImageOrder,
                            ItemImagePath = image.ItemImagePath,
                            ShowImage = image.ShowImage
                        };
                        imageRepository.Add(itemImage);
                    }
                }
            }
            else
            {
                //Delete All Items if List from Client is Empty
                foreach (ItemImage image in  dbList)
                {
                    imageRepository.Delete(image);
                }
            }
            imageRepository.SaveChanges();
        }


        #endregion

        #region Notification Send
        private void SendNotification(ItemVariation itemVariation)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel
            {
                NotificationResponse =
                {
                    TitleE = ConfigurationManager.AppSettings["ItemVariationE"],
                    TitleA = ConfigurationManager.AppSettings["ItemVariationA"],
                    SubCategoryId = 3,//Item Variation
                    AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ItemVariationAlertBefore"]),
                    CategoryId = 7,//Inventory
                    ItemId = itemVariation.ItemVariationId,
                    AlertDate = Convert.ToDateTime(DateTime.Now).ToShortDateString(),
                    AlertDateType = 1,
                    SystemGenerated = true,
                    ForAdmin = false,
                    ForRole = UserRole.InventoryManager//Inventory Manager
                }
            };

            #region Create Notification

            notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);

            #endregion
        }
        #endregion
    }
}
