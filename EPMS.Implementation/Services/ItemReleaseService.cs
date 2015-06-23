﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class ItemReleaseService : IItemReleaseService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IItemReleaseRepository itemReleaseRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IItemReleaseDetailRepository detailRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly IItemReleaseHistoryRepository releaseHistoryRepository;
        private readonly IItemReleaseQuantityRepository releaseQuantityRepository;

        public ItemReleaseService(ICustomerRepository customerRepository, IItemVariationRepository itemVariationRepository, IItemReleaseRepository itemReleaseRepository, IRFIRepository rfiRepository, IOrdersRepository ordersRepository, IItemReleaseDetailRepository detailRepository, IAspNetUserRepository aspNetUserRepository, IItemReleaseHistoryRepository releaseHistoryRepository, IItemReleaseQuantityRepository releaseQuantityRepository)
        {
            this.customerRepository = customerRepository;
            this.itemVariationRepository = itemVariationRepository;
            this.itemReleaseRepository = itemReleaseRepository;
            this.ordersRepository = ordersRepository;
            this.detailRepository = detailRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.releaseHistoryRepository = releaseHistoryRepository;
            this.releaseQuantityRepository = releaseQuantityRepository;
        }

        public IRFCreateResponse GetCreateResponse(long id)
        {
            IRFCreateResponse response = new IRFCreateResponse
            {
                Customers = customerRepository.GetAll(),
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList().ToList(),
                ItemRelease = id != 0 ? itemReleaseRepository.Find(id) : new ItemRelease(),
            };
            IList<RFI> customerRfis = new List<RFI>();
            if (response.ItemRelease.RequesterId != null)
            {
                var customerOrders = ordersRepository.GetOrdersByCustomerId((long)response.ItemRelease.RequesterId);
                
                foreach (var customerOrder in customerOrders)
                {
                    if (customerOrder.RFIs.Any())
                    {
                        foreach (var rfI in customerOrder.RFIs)
                        {
                            customerRfis.Add(rfI);
                        }
                    }
                }
            }
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
                var item = itemReleaseQuantity.Where(x => x.WarehouseId == warehouse.WarehousrId && x.ItemVariationId == warehouse.ItemVariationId);
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
                RecentIrf = itemReleaseRepository.Find((long) parentId)
            };
            response.IrfItems = response.RecentIrf.ItemReleaseDetails;
            if (response.RecentIrf != null)
            {
                if (!string.IsNullOrEmpty(response.RecentIrf.ManagerId))
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
            return itemReleaseRepository.GetAllItemRelease(searchRequest);
        }

        public bool AddItemRelease(ItemRelease itemRelease, List<ItemReleaseDetail> itemDetails)
        {
            try
            {
                itemRelease.ItemReleaseDetails = new List<ItemReleaseDetail>();
                foreach (var itemReleaseDetail in itemDetails)
                {
                    itemRelease.ItemReleaseDetails.Add(itemReleaseDetail);
                }
                itemReleaseRepository.Add(itemRelease);
                itemReleaseRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateItemReleaseStatus(ItemReleaseStatus releaseStatus)
        {
            try
            {
                var itemRelease = itemReleaseRepository.Find(releaseStatus.ItemReleaseId);
                itemRelease.Notes = releaseStatus.Notes;
                itemRelease.NotesAr = releaseStatus.NotesAr;
                itemRelease.ManagerId = releaseStatus.ManagerId;
                if (itemRelease.Status != releaseStatus.Status && releaseStatus.Status != 1)
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
                                dbListQty.RemoveAll(x=>x.ItemReleaseQuantityId == qty.ItemReleaseQuantityId);
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
