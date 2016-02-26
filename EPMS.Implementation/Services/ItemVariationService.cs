using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
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
    internal class ItemVariationService : IItemVariationService
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
        private readonly InventoryDepartmentRepository inventoryDepartmentRepository;
        private readonly IVendorRepository vendorRepository;
        private readonly IVendorItemsRepository itemRepository;
        private readonly IPoItemRepository poItemRepository;

        #endregion

        #region Constructor

        public ItemVariationService(IItemVariationRepository variationRepository, ISizeRepository sizeRepository,
            IManufacturerRepository manufacturerRepository, IStatusRepository statusRepository,
            IColorRepository colorRepository, IItemImageRepository imageRepository,
            IItemManufacturerRepository itemManufacturerRepository, IItemWarehouseRepository itemWarehouseRepository,
            INotificationService notificationService, IWarehouseService warehouseService,
            IInventoryItemRepository inventoryItemRepository, ItemWarehouseService itemWarehouseService,
            ItemReleaseQuantityRepository itemReleaseQuantityRepository,
            InventoryDepartmentRepository inventoryDepartmentRepository, IVendorRepository vendorRepository,
            IVendorItemsRepository itemRepository, IPoItemRepository poItemRepository)
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
            this.inventoryDepartmentRepository = inventoryDepartmentRepository;
            this.vendorRepository = vendorRepository;
            this.itemRepository = itemRepository;
            this.poItemRepository = poItemRepository;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Item Variations
        /// </summary>
        public IEnumerable<ItemVariation> GetAll()
        {
            return variationRepository.GetAll();
        }

        /// <summary>
        /// Find Variation By Id
        /// </summary>
        public ItemVariation FindVariationById(long id)
        {
            return variationRepository.Find(id);
        }

        public IEnumerable<Color> GetAllColors()
        {
            return colorRepository.GetAll();
        }

        public IEnumerable<Size> GetAllSizes()
        {
            return sizeRepository.GetAll();
        }

        /// <summary>
        /// Find Variation By BarCode
        /// </summary>
        public PCFromBarcodeResponse FindVariationByBarcode(string barcode)
        {
            var item = variationRepository.FindVariationByBarcode(barcode);
            if (item != null)
            {

                return new PCFromBarcodeResponse
                {
                    ItemBarcode = barcode,
                    ItemVariationId = item.ItemVariationId,
                    ItemNameEn = item.InventoryItem.ItemNameEn,
                    ItemNameAr = item.InventoryItem.ItemNameAr,
                    SKUDescriptionEn = item.SKUDescriptionEn,
                    SKUDescriptionAr = item.SKUDescriptionAr,
                    ItemsInPackage = item.InventoryItem.QuantityInPackage ?? 0,
                    //TotalItemsCountInWarehouse = item.ItemWarehouses.Sum(x => x.Quantity)
                    TotalItemsCountInWarehouse =
                        item.ItemWarehouses.Sum(x => x.Quantity) - item.ItemReleaseQuantities.Sum(x => x.Quantity),
                };
            }
            return null;
        }

        public long[] GetItemVariationId(string[] items)
        {
            long[] variationIds = new long[items.Length];
            int i = 0;
            foreach (string item in items)
            {
                variationIds.SetValue(variationRepository.GetItemVariationId(item), i);
                i++;
            }
            return variationIds;
        }

        /// <summary>
        /// Add Variation
        /// </summary>
        public bool AddVariation(ItemVariation itemVariation)
        {
            variationRepository.Add(itemVariation);
            variationRepository.SaveChanges();
            return true;
        }

        /// <summary>
        /// Update Variation
        /// </summary>
        public bool UpdateVariation(ItemVariation itemVariation)
        {
            variationRepository.Update(itemVariation);
            variationRepository.SaveChanges();
            return true;
        }

        /// <summary>
        /// Delete Variation
        /// </summary>
        public void DeleteVartiation(ItemVariation itemVariation)
        {
            variationRepository.Delete(itemVariation);
            variationRepository.SaveChanges();
        }

        /// <summary>
        /// Get Variations for Dropdown List
        /// </summary>
        public IEnumerable<ItemVariationDropDownListItem> GetItemVariationDropDownList()
        {
            return variationRepository.GetItemVariationDropDownList();
        }

        /// <summary>
        /// Item Variation Response
        /// </summary>
        public ItemVariationResponse ItemVariationResponse(long id, long itemVariationId)
        {
            ItemVariationResponse response = new ItemVariationResponse();
            response.ItemVariation = id > 0 ? variationRepository.Find(id) : new ItemVariation();
            response.ColorsForDdl = colorRepository.GetAll();
            response.SizesForDdl = sizeRepository.GetAll();
            //response.ManufacturersForDdl = manufacturerRepository.GetAll();
            response.ManufacturersForDdl = vendorRepository.GetAll();
            response.StatusesForDdl = statusRepository.GetAll();
            response.WarehousesForDdl = warehouseService.GetAll();
            response.InventoryItem = inventoryItemRepository.Find(itemVariationId);

            response.PurchaseOrderItems = poItemRepository.GetPoItemsByVarId(id).ToList();
            var manufacturerGroup = response.PurchaseOrderItems.Where(x=>x.PurchaseOrder.Status == 2).GroupBy(x => x.VendorId);

            //If POItems exists against this Variation but not in Item Manufacturer Table
            foreach (var poItem in manufacturerGroup)
            {
                var record = poItem.OrderByDescending(x => x.RecCreatedDate).FirstOrDefault();
                if (response.ItemVariation.ItemManufacturers.All(x => x.ManufacturerId != record.VendorId))
                {
                    if (poItem.FirstOrDefault() != null)
                    {
                        var newManufacturer = new ItemManufacturer
                        {
                            Quantity = record.ItemQty,
                            Price = record.UnitPrice.ToString(),
                            ManufacturerId = (long)record.VendorId,
                            ItemVariationId = (long)record.ItemVariationId,
                            Vendor = vendorRepository.Find((long)record.VendorId),
                            ManuallyAdded = true
                        };
                        response.ItemVariation.ItemManufacturers.Add(newManufacturer);
                    }
                }
            }

            if (response.PurchaseOrderItems != null && response.ItemVariation.ItemManufacturers != null)
                foreach (ItemManufacturer itemManufacturer in response.ItemVariation.ItemManufacturers)
                {
                    int oldQty = 0;
                    if (!itemManufacturer.ManuallyAdded)
                    {
                        oldQty = (int)itemManufacturer.Quantity;
                    }
                    var record =
                        response.PurchaseOrderItems.OrderByDescending(x => x.RecCreatedDate).FirstOrDefault(x => x.VendorId == itemManufacturer.ManufacturerId);
                    if (record != null)
                    {
                        itemManufacturer.Price = record.UnitPrice.ToString();
                        itemManufacturer.Quantity = record.ItemQty;
                    }
                    itemManufacturer.TotalQuantity =
                        (int)response.PurchaseOrderItems.Where(x => x.VendorId == itemManufacturer.ManufacturerId)
                            .Sum(x => x.ItemQty) + oldQty;
                }

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
                    var rifQty = itemWarehouse.ItemVariation.RIFItems.Sum(x => x.ItemQty);
                    var difQty = itemWarehouse.ItemVariation.DIFItems.Sum(x => x.ItemQty);
                    if (itemReleaseQuantity != null)
                    {
                        itemWarehouse.Quantity = itemWarehouse.Quantity + rifQty - (itemReleaseQuantity + difQty);
                    }
                }
            }
            return response;
        }

        public ItemVariationForWarehouse GetItemVariationByWarehouseId(long warehouseId)
        {
            ItemVariationForWarehouse response = new ItemVariationForWarehouse();
            IEnumerable<ItemVariation> itemVariations = variationRepository.GetItemVariationByWarehouseId(warehouseId);
            if (itemVariations != null)
            {
                IEnumerable<ItemVariation> variations = itemVariations as IList<ItemVariation> ??
                                                        itemVariations.ToList();
                response.ItemVariationDropDownListItems =
                    variations
                        .Select(x => new ItemVariationDropDownListItem
                        {
                            ItemVariationId = x.ItemVariationId,
                            ItemCodeSKUCodeDescriptoinEn =
                                x.SKUDescriptionEn + " - " + x.InventoryItem.ItemCode + " - " + x.SKUCode,
                            ItemCodeSKUCodeDescriptoinAr =
                                x.SKUDescriptionAr + " - " + x.InventoryItem.ItemCode + " - " + x.SKUCode,
                            SKUCode = x.SKUCode,
                            ItemSKUDescriptoinEn = x.SKUDescriptionEn,
                            ItemSKUDescriptoinAr = x.SKUDescriptionAr,
                            ItemVariationDescriptionA = x.DescriptionAr,
                            ItemVariationDescriptionE = x.DescriptionEn,
                            ItemNameA = x.InventoryItem.ItemNameAr,
                            ItemNameE = x.InventoryItem.ItemNameEn
                        });
                response.InventoryDepartments = variations.Select(x => x.InventoryItem.InventoryDepartment).Distinct();
            }
            return response;
        }

        public ItemVariationDetail GetItemVariationDetail(long variationId)
        {
            var variation = variationRepository.Find(variationId);
            if (variation != null)
            {
                return new ItemVariationDetail
                {
                    ItemVariationId = variation.ItemVariationId,
                    SKUCode = variation.SKUCode,
                    ItemVariationDescriptionE = variation.DescriptionEn,
                    ItemVariationDescriptionA = variation.DescriptionAr,
                    ItemNameE = variation.InventoryItem.ItemNameEn,
                    ItemNameA = variation.InventoryItem.ItemNameAr,
                    DescriptionForQuotationEn = variation.DescriptionForQuotationEn,
                    DescriptionForQuotationAr = variation.DescriptionForQuotationAr,
                    ItemSKUDescriptoinEn = variation.SKUDescriptionEn,
                    ItemSKUDescriptoinAr = variation.SKUDescriptionAr,
                    UnitPrice = variation.UnitPrice,
                    ItemCodeSKUCode = variation.InventoryItem.ItemCode + " - " + variation.SKUCode
                };
            }
            return new ItemVariationDetail();
        }

        /// <summary>
        /// Get all variations by ItemVariationId
        /// </summary>
        public IEnumerable<ItemVariation> GetVariationsByInventoryItemId(long inventoryItemId)
        {
            return variationRepository.GetVariationsByInventoryItemId(inventoryItemId);
        }

        /// <summary>
        /// Save Item Variation from Client
        /// </summary>
        public ItemVariationResponse SaveItemVariation(ItemVariationRequest variationToSave)
        {
            variationToSave.ItemVariation.InventoryItem =
                inventoryItemRepository.Find(variationToSave.ItemVariation.InventoryItemId);
            ItemVariation itemVariationFromDatabase =
                variationRepository.Find(variationToSave.ItemVariation.ItemVariationId);
            var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            if (variationToSave.ItemVariation.ItemVariationId > 0)
            {
                UpdateItemVariation(variationToSave, itemVariationFromDatabase);
                //UpdateItemVariation(variationToSave.ItemVariation);
                UpdateSizeList(variationToSave, itemVariationFromDatabase);
                UpdateManufacturerList(variationToSave);
                UpdateWarehouseList(variationToSave);
                UpdateStatusList(variationToSave, itemVariationFromDatabase);
                UpdateColorList(variationToSave, itemVariationFromDatabase);
                UpdateImages(variationToSave, itemVariationFromDatabase);
            }
            else
            {
                AddNewVariation(variationToSave);
                AddSizeList(variationToSave);
                AddManufacturerList(variationToSave);
                AddWarehouseList(variationToSave);
                AddStatusList(variationToSave);
                AddColorList(variationToSave);
                AddImages(variationToSave);
            }
            variationRepository.SaveChanges();
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;
            return new ItemVariationResponse
            {
                ItemVariation = new ItemVariation
                {
                    ItemVariationId = variationToSave.ItemVariation.ItemVariationId
                }
            };
        }

        /// <summary>
        /// Add New Variation from Client
        /// </summary>
        private void AddNewVariation(ItemVariationRequest variationToSave)
        {
            //CultureInfo culture = new CultureInfo("en-US");
            //var datetimenow = DateTime.Now.ToShortDateString();
            // var dateNow = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
            //var datenow = DateTime.ParseExact(dateNow, "dd/MM/yyyy", new CultureInfo("en"));

            // Specify exactly how to interpret the string.
            variationToSave.ItemVariation.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            variationToSave.ItemVariation.RecCreatedDt = DateTime.Now;
            variationToSave.ItemVariation.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            variationToSave.ItemVariation.RecLastUpdatedDt = DateTime.Now;
            string itemNameEn;
            string itemNameAr;
            itemNameEn = variationToSave.ItemVariation.InventoryItem.ItemNameEn.Length < 6
                ? variationToSave.ItemVariation.InventoryItem.ItemNameEn
                : variationToSave.ItemVariation.InventoryItem.ItemNameEn.Substring(0, 6);
            itemNameAr = variationToSave.ItemVariation.InventoryItem.ItemNameAr.Length < 6
                ? variationToSave.ItemVariation.InventoryItem.ItemNameAr
                : variationToSave.ItemVariation.InventoryItem.ItemNameAr.Substring(0, 6);
            Color color = colorRepository.Find(Convert.ToInt64(variationToSave.ColorArrayList));
            var colorEn = "Col";
            var colorAr = "Col";
            if (color != null)
            {
                colorEn = color.ColorNameEn.Length < 3 ? color.ColorNameEn : color.ColorNameEn.Substring(0, 3);
                colorAr = color.ColorNameAr.Length < 3 ? color.ColorNameAr : color.ColorNameAr.Substring(0, 3);
            }
            Size size = sizeRepository.Find(Convert.ToInt64(variationToSave.SizeArrayList));
            var sizeEn = "Siz";
            var sizeAr = "Siz";
            if (size != null)
            {
                sizeEn = size.SizeNameEn.Length < 3 ? size.SizeNameEn : size.SizeNameEn.Substring(0, 3);
                sizeAr = size.SizeNameAr.Length < 3 ? size.SizeNameAr : size.SizeNameAr.Substring(0, 3);
            }
            InventoryDepartment department = null;
            var deptnameEn = "Dep";
            var deptnameAr = "Dep";
            if (variationToSave.ItemVariation.InventoryItem.DepartmentId != null)
            {
                department =
                    inventoryDepartmentRepository.Find((long)variationToSave.ItemVariation.InventoryItem.DepartmentId);
            }
            if (department != null)
            {
                deptnameEn = department.DepartmentNameEn.Length < 3
                    ? department.DepartmentNameEn
                    : department.DepartmentNameEn.Substring(0, 3);
                deptnameAr = department.DepartmentNameAr.Length < 3
                    ? department.DepartmentNameAr
                    : department.DepartmentNameAr.Substring(0, 3);
            }
            Status status = statusRepository.Find(Convert.ToInt64(variationToSave.StatusArrayList));
            var statusEn = "Sta";
            var statusAr = "Sta";
            if (status != null)
            {
                statusEn = status.StatusNameEn.Length < 3 ? status.StatusNameEn : status.StatusNameEn.Substring(0, 3);
                statusAr = status.StatusNameAr.Length < 3 ? status.StatusNameAr : status.StatusNameAr.Substring(0, 3);
            }
            variationToSave.ItemVariation.SKUDescriptionEn = itemNameEn + deptnameEn + colorEn + sizeEn + statusEn;
            variationToSave.ItemVariation.SKUDescriptionAr = itemNameAr + deptnameAr + colorAr + sizeAr + statusAr;

            var qtyFromManufacturer = variationToSave.ItemManufacturers.Sum(x => x.Quantity);
            var priceFromManufacturer = variationToSave.ItemManufacturers.Sum(x => Convert.ToInt64(x.Price));

            var qtyInHand = Convert.ToDouble(variationToSave.ItemVariation.QuantityInHand) +
                            variationToSave.ItemManufacturers.Sum(x => x.Quantity);
            variationToSave.ItemVariation.UnitCost = Convert.ToDouble(qtyInHand)/priceFromManufacturer;

            variationRepository.Add(variationToSave.ItemVariation);
            //Item variation Notification
            SendNotification(variationToSave.ItemVariation);
        }

        /// <summary>
        /// Update Existing Variation from Client
        /// </summary>
        private void UpdateItemVariation(ItemVariationRequest variationToSave, ItemVariation itemVariationFromDatabase)
        {
            variationToSave.ItemVariation.RecCreatedDt = itemVariationFromDatabase.RecCreatedDt;
            variationToSave.ItemVariation.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            variationToSave.ItemVariation.RecLastUpdatedDt = DateTime.Now;
            string itemNameEn;
            string itemNameAr;
            itemNameEn = variationToSave.ItemVariation.InventoryItem.ItemNameEn.Length < 6
                ? variationToSave.ItemVariation.InventoryItem.ItemNameEn
                : variationToSave.ItemVariation.InventoryItem.ItemNameEn.Substring(0, 6);
            itemNameAr = variationToSave.ItemVariation.InventoryItem.ItemNameAr.Length < 6
                ? variationToSave.ItemVariation.InventoryItem.ItemNameAr
                : variationToSave.ItemVariation.InventoryItem.ItemNameAr.Substring(0, 6);
            Color color = colorRepository.Find(Convert.ToInt64(variationToSave.ColorArrayList));
            var colorEn = "Col";
            var colorAr = "Col";
            if (color != null)
            {
                colorEn = color.ColorNameEn.Length < 3 ? color.ColorNameEn : color.ColorNameEn.Substring(0, 3);
                colorAr = color.ColorNameAr.Length < 3 ? color.ColorNameAr : color.ColorNameAr.Substring(0, 3);
            }
            Size size = sizeRepository.Find(Convert.ToInt64(variationToSave.SizeArrayList));
            var sizeEn = "Siz";
            var sizeAr = "Siz";
            if (size != null)
            {
                sizeEn = size.SizeNameEn.Length < 3 ? size.SizeNameEn : size.SizeNameEn.Substring(0, 3);
                sizeAr = size.SizeNameAr.Length < 3 ? size.SizeNameAr : size.SizeNameAr.Substring(0, 3);
            }
            InventoryDepartment department = null;
            var deptnameEn = "Dep";
            var deptnameAr = "Dep";
            if (variationToSave.ItemVariation.InventoryItem.DepartmentId != null)
            {
                department =
                    inventoryDepartmentRepository.Find((long)variationToSave.ItemVariation.InventoryItem.DepartmentId);
            }
            if (department != null)
            {
                deptnameEn = department.DepartmentNameEn.Length < 3
                    ? department.DepartmentNameEn
                    : department.DepartmentNameEn.Substring(0, 3);
                deptnameAr = department.DepartmentNameAr.Length < 3
                    ? department.DepartmentNameAr
                    : department.DepartmentNameAr.Substring(0, 3);
            }
            Status status = statusRepository.Find(Convert.ToInt64(variationToSave.StatusArrayList));
            var statusEn = "Sta";
            var statusAr = "Sta";
            if (status != null)
            {
                statusEn = status.StatusNameEn.Length < 3 ? status.StatusNameEn : status.StatusNameEn.Substring(0, 3);
                statusAr = status.StatusNameAr.Length < 3 ? status.StatusNameAr : status.StatusNameAr.Substring(0, 3);
            }
            variationToSave.ItemVariation.SKUDescriptionEn = itemNameEn + deptnameEn + colorEn + sizeEn + statusEn;
            variationToSave.ItemVariation.SKUDescriptionAr = itemNameAr + deptnameAr + colorAr + sizeAr + statusAr;


            variationRepository.Update(variationToSave.ItemVariation);
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
                        PlaceInWarehouse = itemWarehouse.PlaceInWarehouse,
                        WarehouseDetailId = itemWarehouse.WarehouseDetailId
                    };
                    itemWarehouseRepository.Add(warehouse);
                }
                //itemWarehouseRepository.SaveChanges();
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
                    if (itemWarehouse.WarehouseId > 0 && itemWarehouse.ItemVariationId > 0)
                    {
                        //Update Items
                        itemWarehouseRepository.Update(itemWarehouse);
                    }
                    else
                    {
                        ItemWarehouse itemToAdd = new ItemWarehouse
                        {
                            ItemVariationId = variationToSave.ItemVariation.ItemVariationId,
                            WarehouseId = itemWarehouse.WarehouseId,
                            Quantity = itemWarehouse.Quantity,
                            PlaceInWarehouse = itemWarehouse.PlaceInWarehouse,
                            WarehouseDetailId = itemWarehouse.WarehouseDetailId
                        };
                        itemWarehouseRepository.Add(itemToAdd);
                    }
                }

                //Delete Items from DB List which are not in Client List
                foreach (ItemWarehouse warehouseItem in dbList)
                {
                    if (clientList.Any(x => x.WarehouseId == warehouseItem.WarehouseId))
                        continue;
                    var itemToDelete =
                        itemWarehouseRepository.FindItemWarehouseByVariationAndManufacturerId(
                            warehouseItem.ItemVariationId, warehouseItem.WarehouseId);
                    itemWarehouseRepository.Delete(itemToDelete);
                }
            }
            else
            {
                //Delete All Items if List from Client is Empty
                foreach (ItemWarehouse warehouseItem in dbList)
                {
                    var itemToDelete =
                        itemWarehouseRepository.FindItemWarehouseByVariationAndManufacturerId(
                            warehouseItem.ItemVariationId, warehouseItem.WarehouseId);
                    itemWarehouseRepository.Delete(itemToDelete);
                }
            }
            //itemWarehouseRepository.SaveChanges();
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
                        ManufacturerId = itemManufacturer.ManufacturerId,
                        Quantity = itemManufacturer.Quantity
                    };
                    itemManufacturerRepository.Add(manufacturer);
                }
                //itemManufacturerRepository.SaveChanges();
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
                            Quantity = itemManufacturer.Quantity
                        };
                        itemManufacturerRepository.Add(itemToAdd);
                    }

                    //Delete Items from DB List which are not in Client List
                    foreach (ItemManufacturer manufacturerItem in dbList)
                    {
                        if (clientList.Any(x => x.ManufacturerId == manufacturerItem.ManufacturerId))
                            continue;
                        var itemToDelete =
                            itemManufacturerRepository.FindItemManufacturerByVariationAndManufacturerId(
                                manufacturerItem.ItemVariationId, manufacturerItem.ManufacturerId);
                        itemManufacturerRepository.Delete(itemToDelete);
                    }
                }
            }
            else
            {
                //Delete All Items if List from Client is Empty
                foreach (ItemManufacturer manufacturerItem in dbList)
                {
                    var itemToDelete =
                        itemManufacturerRepository.FindItemManufacturerByVariationAndManufacturerId(
                            manufacturerItem.ItemVariationId, manufacturerItem.ManufacturerId);
                    itemManufacturerRepository.Delete(itemToDelete);
                }
            }
            //itemManufacturerRepository.SaveChanges();
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

        /// <summary>
        /// Add Images List from Client
        /// </summary>
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
                //imageRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Update Images List from Client
        /// </summary>
        private void UpdateImages(ItemVariationRequest variationToSave, ItemVariation itemVariationFromDatabase)
        {
            IEnumerable<ItemImage> dbList = itemVariationFromDatabase.ItemImages.ToList();
            IEnumerable<ItemImage> clientList = variationToSave.ItemImages;

            if (clientList != null && clientList.Count() > 0)
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
                //Delete Items that were removed from ClientList
                foreach (ItemImage image in dbList)
                {
                    if (clientList.All(x => x.ImageId != image.ImageId))
                    {
                        imageRepository.Delete(image);
                    }
                }
            }
            else
            {
                //Delete All Items if List from Client is Empty
                foreach (ItemImage image in dbList)
                {
                    imageRepository.Delete(image);
                }
            }
            //imageRepository.SaveChanges();
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
                    SubCategoryId = 3, //Item Variation
                    AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ItemVariationAlertBefore"]),
                    CategoryId = 7, //Inventory
                    ItemId = itemVariation.ItemVariationId,
                    AlertDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en")),
                    AlertDateType = 1,
                    SystemGenerated = true,
                    ForAdmin = false,
                    ForRole = UserRole.InventoryManager //Inventory Manager
                }
            };

            #region Create Notification

            notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);

            #endregion
        }

        #endregion
    }
}