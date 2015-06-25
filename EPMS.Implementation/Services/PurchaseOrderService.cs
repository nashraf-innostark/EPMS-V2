using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Implementation.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository repository;
        private readonly IPoItemRepository itemRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly INotificationService notificationService;
        private readonly IAspNetUserRepository aspNetUserRepository;

        public PurchaseOrderService(IItemVariationRepository itemVariationRepository,INotificationService notificationService, IPurchaseOrderRepository repository, IAspNetUserRepository aspNetUserRepository, IPoItemRepository itemRepository)
        {
            this.itemVariationRepository = itemVariationRepository;
            this.notificationService = notificationService;
            this.repository = repository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.itemRepository = itemRepository;
        }

        private void PoItemUpdation(PurchaseOrder order)
        {
            var itemsInDb = itemRepository.GetPoItemsByPoId(order.PurchaseOrderId).ToList();

            foreach (var item in order.PurchaseOrderItems)
            {
                if (item.ItemId > 0)
                {
                    //update
                    var itemInDb = itemsInDb.Where(x => x.ItemId == item.ItemId);
                    if (itemInDb != null)
                    {
                        itemRepository.Update(item);
                        itemRepository.SaveChanges();
                        itemsInDb.RemoveAll(x => x.ItemId == item.ItemId);
                    }
                }
                else
                {
                    //save
                    itemRepository.Add(item);
                    itemRepository.SaveChanges();
                }
            }
            DeletePoItem(itemsInDb);
        }
        private void DeletePoItem(IEnumerable<PurchaseOrderItem> itemsInDb)
        {
            foreach (var item in itemsInDb)
            {
                itemRepository.Delete(item);
                itemRepository.SaveChanges();
            }
        }

        public POCreateResponse GetPoResponseData(long? id)
        {
            POCreateResponse response = new POCreateResponse
            {
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList()
            };
            if (id == null) return response;
            var po = repository.Find((long)id);
            if (po == null) return response;

            response.Order = po;
            response.OrderItems = po.PurchaseOrderItems;
            if (!string.IsNullOrEmpty(po.RecCreatedBy))
            {
                var employee = aspNetUserRepository.Find(po.RecCreatedBy).Employee;
                response.RequesterNameE = employee != null ? employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE : "";
                response.RequesterNameA = employee != null ? employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA : "";
            }
            
            return response;
        }

        public PurchaseOrder FindPoById(long id, string from)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            if (from == "History")
            {
                
            }
            else
            {
                purchaseOrder = repository.Find(id);
            }
            return purchaseOrder;
        }

        public PurchaseOrderListResponse GetAllPoS(PurchaseOrderSearchRequest searchRequest)
        {
            return repository.GetAllPoS(searchRequest);
        }

        public bool SavePO(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder.PurchaseOrderId > 0)
            {
                //update
                if (UpdatePO(purchaseOrder))//if RFI updated, then update the items
                    PoItemUpdation(purchaseOrder);
            }
            else
            {
                //save
                return AddPO(purchaseOrder);
            }
            return true;
        }

        public bool AddPO(PurchaseOrder purchaseOrder)
        {
            try
            {
                repository.Add(purchaseOrder);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePO(PurchaseOrder purchaseOrder)
        {
            try
            {
                repository.Update(purchaseOrder);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePOStatus(PurchaseOrderStatus purchaseOrder)
        {
            return true;
        }

        public void DeletePO(PurchaseOrder purchaseOrder)
        {
            repository.Delete(purchaseOrder);
            repository.SaveChanges();
        }

        private void SendNotification(PurchaseOrder purchaseOrder)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();

            #region RFI
            notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["PurchaseOrderE"];
            notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["PurchaseOrderA"];

            notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["PurchaseOrderAlertBefore"]); //Days

            notificationViewModel.NotificationResponse.CategoryId = 7; //Inventory
            notificationViewModel.NotificationResponse.SubCategoryId = 7; //PO Accepted (WH Manager)

            notificationViewModel.NotificationResponse.ItemId = purchaseOrder.PurchaseOrderId;
            notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(DateTime.Now).ToShortDateString(); //Actual event date
            notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
            notificationViewModel.NotificationResponse.SystemGenerated = true;
            notificationViewModel.NotificationResponse.ForAdmin = false;
            notificationViewModel.NotificationResponse.ForRole = UserRole.WarehouseManager;//WH Manager

            notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);

            #endregion
        }
    }
}
