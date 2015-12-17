using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Implementation.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository repository;
        private readonly IPOHistoryRepository historyRepository;
        private readonly IPoItemRepository itemRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly INotificationService notificationService;
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly IVendorRepository vendorRepository;

        public PurchaseOrderService(IItemVariationRepository itemVariationRepository,INotificationService notificationService, IPurchaseOrderRepository repository, IAspNetUserRepository aspNetUserRepository, IPoItemRepository itemRepository, IVendorRepository vendorRepository, IPOHistoryRepository historyRepository)
        {
            this.itemVariationRepository = itemVariationRepository;
            this.notificationService = notificationService;
            this.repository = repository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.itemRepository = itemRepository;
            this.vendorRepository = vendorRepository;
            this.historyRepository = historyRepository;
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
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownListItems()
            };
            if (id == null)
            {
                response.LastFormNumber = repository.GetLastFormNumber();
                return response;
            }
            var po = repository.Find((long)id);
            if (po == null) return response;

            response.Order = po;
            response.OrderItems = po.PurchaseOrderItems;
            if (po.AspNetUser != null && po.AspNetUser.Employee != null)
            {
                var employee = po.AspNetUser.Employee;
                response.RequesterNameE = employee != null ? employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE : "";
                response.RequesterNameA = employee != null ? employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA : "";
            }
            
            return response;
        }

        public PODetailResponse GetPoDetailResponse(long id, string from)
        {
            PODetailResponse response;
            if (from == "History")
            {
                response = new PODetailResponse
                {
                    PurchaseOrder = historyRepository.Find(id).CreateFromPoHistoryToPo()
                };
            }
            else
            {
                response = new PODetailResponse
                {
                    PurchaseOrder = repository.Find(id)
                };
            }
            response.OrderItems = response.PurchaseOrder.PurchaseOrderItems;
            response.Vendors = vendorRepository.GetAll();
            return response;
        }

        public PoHistoryResponse GetPoHistoryData(long? parentId)
        {
            if (parentId == null)
            {
                return new PoHistoryResponse();
            }
            var pos = historyRepository.GetPoHistoryData((long)parentId);
            var poList = pos as IList<PurchaseOrderHistory> ?? pos.ToList();
            PoHistoryResponse response = new PoHistoryResponse
            {
                PurchaseOrders = poList.Select(x => x.CreateFromPoHistoryToPo()),
                RecentPo = repository.Find((long)parentId)
            };
            response.PoItems = response.RecentPo.PurchaseOrderItems;

            if (response.RecentPo != null)
            {
                if (response.RecentPo.Manager != null)
                {
                    var manager = response.RecentPo.Manager.Employee;
                    response.ManagerNameEn = manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " +
                                           manager.EmployeeLastNameE;
                    response.ManagerNameAr = manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " +
                                           manager.EmployeeLastNameA;
                }
                if (response.RecentPo.AspNetUser != null)
                {
                    var employee = response.RecentPo.AspNetUser.Employee;
                    response.RequesterNameEn = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " +
                                           employee.EmployeeLastNameE;
                    response.RequesterNameAr = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " +
                                           employee.EmployeeLastNameA;
                }
            }
            response.Vendors = vendorRepository.GetAll();
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
                if (UpdatePO(purchaseOrder)) //if RFI updated, then update the items
                {
                    PoItemUpdation(purchaseOrder);
                    if (purchaseOrder.Status == 1)
                    {
                        SendNotification(purchaseOrder);
                    }
                }
            }
            else
            {
                //save
                return AddPO(purchaseOrder);
            }
            if (purchaseOrder.Status != 3)
            {
                var poHistoryToAdd = purchaseOrder.CreateFromPoToPoHistory();
                historyRepository.Add(poHistoryToAdd);
                historyRepository.SaveChanges();
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

        public IEnumerable<PurchaseOrder> GetRecentPOs(int status, string requester, DateTime date)
        {
            return repository.GetRecentPOs(status, requester, date);
        }

        public IEnumerable<PurchaseOrder> FindPoByVendorId(long? vendorId)
        {
            if (vendorId != null)
            {
                return repository.FindPoByVendorId((long) vendorId);
            }
            return new List<PurchaseOrder>();
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
            notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en")); //Actual event date
            notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
            notificationViewModel.NotificationResponse.SystemGenerated = true;
            notificationViewModel.NotificationResponse.ForAdmin = false;
            notificationViewModel.NotificationResponse.ForRole = UserRole.WarehouseManager;//WH Manager

            notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);

            #endregion
        }
    }
}
