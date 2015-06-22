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
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository repository;
        private readonly IPoItemRepository itemRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

        public PurchaseOrderService(IItemVariationRepository itemVariationRepository, IPurchaseOrderRepository repository, IAspNetUserRepository aspNetUserRepository, IPoItemRepository itemRepository)
        {
            this.itemVariationRepository = itemVariationRepository;
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

        public void DeletePO(PurchaseOrder purchaseOrder)
        {
            repository.Delete(purchaseOrder);
            repository.SaveChanges();
        }
    }
}
