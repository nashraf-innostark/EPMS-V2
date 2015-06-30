using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;
using FaceSharp.Api.Objects;
using Microsoft.Practices.EnterpriseLibrary.Common.Properties;

namespace EPMS.Implementation.Services
{
    public class ItemReleaseService : IItemReleaseService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IItemWarehouseRepository itemWarehouseRepository;
        private readonly INotificationService notificationService;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IItemReleaseRepository itemReleaseRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IItemReleaseDetailRepository detailRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly IItemReleaseHistoryRepository releaseHistoryRepository;
        private readonly IItemReleaseQuantityRepository releaseQuantityRepository;

        public ItemReleaseService(IItemWarehouseRepository itemWarehouseRepository, INotificationService notificationService, IItemVariationRepository itemVariationRepository, IItemReleaseRepository itemReleaseRepository, IRFIRepository rfiRepository, IOrdersRepository ordersRepository, IItemReleaseDetailRepository detailRepository, IAspNetUserRepository aspNetUserRepository, IItemReleaseHistoryRepository releaseHistoryRepository, IItemReleaseQuantityRepository releaseQuantityRepository, IEmployeeRepository employeeRepository)
        {
            this.itemWarehouseRepository = itemWarehouseRepository;
            this.notificationService = notificationService;
            this.itemVariationRepository = itemVariationRepository;
            this.itemReleaseRepository = itemReleaseRepository;
            this.ordersRepository = ordersRepository;
            this.detailRepository = detailRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.releaseHistoryRepository = releaseHistoryRepository;
            this.releaseQuantityRepository = releaseQuantityRepository;
            this.employeeRepository = employeeRepository;
        }

        public IRFCreateResponse GetCreateResponse(long id)
        {
            IRFCreateResponse response = new IRFCreateResponse
            {
                Employees = employeeRepository.GetAll().Where(x => x.AspNetUsers.Count > 0),
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList().ToList(),
                ItemRelease = id != 0 ? itemReleaseRepository.Find(id) : new ItemRelease(),
            };
            IList<RFI> customerRfis = new List<RFI>();
            response.Rfis = customerRfis;
            var itemVariations = itemVariationRepository.GetAll();
            response.ItemWarehouses = new List<ItemWarehouse>();
            foreach (var itemVariation in itemVariations)
            {
                foreach (var itemWarehouse in itemVariation.ItemWarehouses)
                {
                    response.ItemWarehouses.Add(itemWarehouse);
                }
            }
            var itemReleaseQuantity = releaseQuantityRepository.GetAll();
            foreach (var itemWarehouse in response.ItemWarehouses)
            {
                ItemWarehouse warehouse = itemWarehouse;
                var item = itemReleaseQuantity.Where(x => x.WarehouseId == warehouse.WarehouseId && x.ItemVariationId == warehouse.ItemVariationId);
                var quantity = item.Sum(x => x.Quantity);
                if (quantity != null)
                {
                    itemWarehouse.Quantity -= quantity;
                }
            }
            return response;
        }

        public ItemRelease FindItemReleaseById(long id, string from)
        {
            ItemRelease irf = null;
            if (from == "History")
            {
                irf = releaseHistoryRepository.Find(id).CreateFromIrfHistoryToIrf();
            }
            else
            {
                irf = itemReleaseRepository.Find(id);
            }
            return irf;
        }

        public IEnumerable<ItemRelease> GetAll()
        {
            return itemReleaseRepository.GetAll();
        }

        public IrfHistoryResponse GetIrfHistoryData(long? parentId)
        {
            if (parentId == null)
            {
                return new IrfHistoryResponse();
            }
            var irfs = releaseHistoryRepository.GetIrfHistoryData((long)parentId);
            var irfList = irfs as IList<ItemReleaseHistory> ?? irfs.ToList();

            IrfHistoryResponse response = new IrfHistoryResponse
            {
                Irfs = irfList.Select(x => x.CreateFromIrfHistoryToIrf()),
                RecentIrf = itemReleaseRepository.Find((long)parentId)
            };
            response.IrfItems = response.RecentIrf.ItemReleaseDetails;
            if (response.RecentIrf != null)
            {
                if (!string.IsNullOrEmpty(response.RecentIrf.ManagerId) && response.RecentIrf.Manager.Employee != null)
                {
                    var manager = response.RecentIrf.Manager.Employee;
                    response.ManagerNameEn = manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " +
                                           manager.EmployeeLastNameE;
                    response.ManagerNameAr = manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " +
                                           manager.EmployeeLastNameA;
                }
                var employee = aspNetUserRepository.Find(response.RecentIrf.RecCreatedBy).Employee;
                response.RequesterNameEn = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " +
                                       employee.EmployeeLastNameE;
                response.RequesterNameAr = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " +
                                       employee.EmployeeLastNameA;
            }
            return response;
        }

        public ItemReleaseResponse GetAllItemRelease(ItemReleaseSearchRequest searchRequest)
        {
            var irfResponse = itemReleaseRepository.GetAllItemRelease(searchRequest);
            return irfResponse;
        }

        public bool AddItemRelease(ItemRelease itemRelease, List<ItemReleaseDetail> itemDetails)
        {
            try
            {
                itemRelease.ItemReleaseDetails = new List<ItemReleaseDetail>();
                foreach (var itemReleaseDetail in itemDetails)
                {
                    itemRelease.ItemReleaseDetails.Add(itemReleaseDetail);

                    //check item remaining Qty
                    foreach (var itemReleaseQuantity in itemReleaseDetail.ItemReleaseQuantities)
                    {
                        var itemQty = itemWarehouseRepository.GetItemQuantity(Convert.ToInt64(itemReleaseDetail.ItemVariationId), itemReleaseQuantity.WarehouseId);
                        var itemReleasedQty = releaseQuantityRepository.GetItemReleasedQuantity(Convert.ToInt64(itemReleaseDetail.ItemVariationId), itemReleaseQuantity.WarehouseId);
                        var itemAvailableQty = itemQty - (itemReleasedQty + itemReleaseDetail.ItemQty);
                        if (itemAvailableQty <= 1)
                        {
                            //Send notification to Inventory Manager about short in-hand inventory

                            #region Send Notification to Inventory Manager

                            SendNotificationAboutShortInventory(itemRelease);

                            #endregion
                        }
                    }
                }
                itemReleaseRepository.Add(itemRelease);
                itemReleaseRepository.SaveChanges();



                //Send notification
                SendNotification(itemRelease);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SendNotificationAboutShortInventory(ItemRelease itemRelease)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel
            {
                NotificationResponse =
                {
                    TitleE = ConfigurationManager.AppSettings["ItemShortOnInventoryE"],
                    TitleA = ConfigurationManager.AppSettings["ItemShortOnInventoryA"],
                    AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ItemShortOnInventoryAlertBefore"]),
                    CategoryId = 7,
                    SubCategoryId = 4, //4.	Item Quantity
                    ItemId = itemRelease.ItemReleaseId,
                    AlertDate = Convert.ToDateTime(DateTime.Now).ToShortDateString(),
                    AlertDateType = 1,
                    SystemGenerated = true,
                    ForAdmin = false,
                    ForRole = UserRole.InventoryManager //inventory manager
                }
            };
            notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
        }

        public bool UpdateItemReleaseStatus(ItemReleaseStatus releaseStatus)
        {
            try
            {
                var itemRelease = itemReleaseRepository.Find(releaseStatus.ItemReleaseId);
                itemRelease.Notes = releaseStatus.Notes;
                itemRelease.NotesAr = releaseStatus.NotesAr;
                itemRelease.ManagerId = releaseStatus.ManagerId;
                itemRelease.RecUpdatedBy = releaseStatus.RecUpdatedBy;
                itemRelease.RecUpdatedDate = releaseStatus.RecUpdatedDate;
                if (itemRelease.Status != releaseStatus.Status && releaseStatus.Status != 3)
                {
                    itemRelease.Status = releaseStatus.Status;
                    var historyToAdd = itemRelease.CreateFromIrfToIrfHistory();
                    releaseHistoryRepository.Add(historyToAdd);
                    releaseHistoryRepository.SaveChanges();
                }
                itemReleaseRepository.Update(itemRelease);
                itemReleaseRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateItemRelease(ItemRelease itemRelease, List<ItemReleaseDetail> clientItems)
        {
            try
            {
                var freshCopyOfItem = itemReleaseRepository.Find(itemRelease.ItemReleaseId);
                ItemRelease itemToSave = freshCopyOfItem;
                List<ItemReleaseDetail> dbItems = freshCopyOfItem.ItemReleaseDetails.ToList();
                // items that newly added
                foreach (var itemReleaseDetail in clientItems)
                {
                    var aa = dbItems.Find(x => x.IRFDetailId == itemReleaseDetail.IRFDetailId);
                    var dbListQty = new List<ItemReleaseQuantity>();
                    if (aa != null)
                    {
                        dbListQty = aa.ItemReleaseQuantities.ToList();
                    }

                    if (dbItems.All(x => x.IRFDetailId != itemReleaseDetail.IRFDetailId))
                    {
                        // Add new
                        itemReleaseDetail.ItemReleaseId = itemRelease.ItemReleaseId;
                        detailRepository.Add(itemReleaseDetail);
                        detailRepository.SaveChanges();
                        //itemToSave.ItemReleaseDetails.Add(itemReleaseDetail);
                    }
                    if (itemReleaseDetail.IRFDetailId > 0)
                    {
                        // Update
                        itemReleaseDetail.ItemReleaseId = itemRelease.ItemReleaseId;
                        detailRepository.Update(itemReleaseDetail);
                        detailRepository.SaveChanges();
                    }
                    foreach (var qty in itemReleaseDetail.ItemReleaseQuantities)
                    {
                        if (qty.ItemReleaseQuantityId > 0)
                        {
                            // Update
                            ItemReleaseQuantity qty1 = qty;
                            var quantityInDb = dbListQty.Where(x => x.ItemReleaseQuantityId == qty1.ItemReleaseQuantityId);
                            if (quantityInDb != null)
                            {
                                releaseQuantityRepository.Update(qty);
                                releaseQuantityRepository.SaveChanges();
                                dbListQty.RemoveAll(x => x.ItemReleaseQuantityId == qty.ItemReleaseQuantityId);
                            }
                        }
                        else
                        {
                            if (qty.ItemVariationId > 0)
                            {
                                // Add
                                releaseQuantityRepository.Add(qty);
                                releaseQuantityRepository.SaveChanges();
                            }
                        }
                    }
                    DeleteRemainingItemQuantities(dbListQty);
                }
                foreach (var itemReleaseDetail in dbItems)
                {
                    if (clientItems.All(x => x.IRFDetailId != itemReleaseDetail.IRFDetailId))
                    {
                        detailRepository.Delete(itemReleaseDetail);
                        detailRepository.SaveChanges();
                        //itemToSave.ItemReleaseDetails.Remove(itemReleaseDetail);
                    }
                }
                itemReleaseRepository.Update(itemRelease);
                itemReleaseRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteItemRelease(ItemRelease itemRelease)
        {
            itemReleaseRepository.Delete(itemRelease);
            itemReleaseRepository.SaveChanges();
        }

        public IEnumerable<ItemRelease> GetRecentIRFs(int status, string requester, DateTime date)
        {
            return itemReleaseRepository.GetRecentIRFs(status, requester, date);
        }


        private void SendNotification(ItemRelease itemRelease, bool isUpdated = false)
        {
            var requesterId = aspNetUserRepository.Find(itemRelease.RequesterId).EmployeeId.ToString();
            #region Item Release For Warehouse Manager

            NotificationViewModel notificationViewModel = new NotificationViewModel
            {
                NotificationResponse =
                {
                    TitleE = ConfigurationManager.AppSettings["ItemReleaseE"],
                    TitleA = ConfigurationManager.AppSettings["ItemReleaseA"],
                    AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ItemReleaseAlertBefore"]),
                    CategoryId = 7,
                    SubCategoryId = 5, //For Warehouse Manager
                    ItemId = itemRelease.ItemReleaseId,
                    AlertDate = Convert.ToDateTime(DateTime.Now).ToShortDateString(),
                    AlertDateType = 1,
                    SystemGenerated = true,
                    ForAdmin = false,
                    ForRole = UserRole.WarehouseManager //warehouse manager
                }
            };



            notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);

            #endregion

            #region Item Release For Requester (Employee)

            notificationViewModel = new NotificationViewModel
            {
                NotificationResponse =
                {
                    TitleE = ConfigurationManager.AppSettings["ItemReleaseE"],
                    TitleA = ConfigurationManager.AppSettings["ItemReleaseA"],
                    AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ItemReleaseAlertBefore"]),
                    CategoryId = 7,
                    SubCategoryId = 6, //For requester
                    ItemId = itemRelease.ItemReleaseId,
                    AlertDate = Convert.ToDateTime(DateTime.Now).ToShortDateString(),
                    AlertDateType = 1,
                    SystemGenerated = true,
                    ForAdmin = false,
                    ForRole = UserRole.Employee, //Employee,
                    EmployeeId = Convert.ToInt64(requesterId)
                }
            };

            notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);

            #endregion
        }

        private void DeleteRemainingItemQuantities(IEnumerable<ItemReleaseQuantity> dbListQty)
        {
            foreach (var itemReleaseQuantity in dbListQty)
            {
                if (itemReleaseQuantity.ItemVariationId > 0)
                {
                    releaseQuantityRepository.Delete(itemReleaseQuantity);
                    releaseQuantityRepository.SaveChanges();
                }
            }

        }
    }
}
