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
    public class ItemReleaseService : IItemReleaseService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IItemReleaseRepository itemReleaseRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IItemReleaseDetailRepository detailRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

        public ItemReleaseService(ICustomerRepository customerRepository, IItemVariationRepository itemVariationRepository, IItemReleaseRepository itemReleaseRepository, IRFIRepository rfiRepository, IOrdersRepository ordersRepository, IItemReleaseDetailRepository detailRepository, IAspNetUserRepository aspNetUserRepository)
        {
            this.customerRepository = customerRepository;
            this.itemVariationRepository = itemVariationRepository;
            this.itemReleaseRepository = itemReleaseRepository;
            this.ordersRepository = ordersRepository;
            this.detailRepository = detailRepository;
            this.aspNetUserRepository = aspNetUserRepository;
        }

        public IRFCreateResponse GetCreateResponse(long id)
        {
            IRFCreateResponse response = new IRFCreateResponse
            {
                Customers = customerRepository.GetAll(),
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList().ToList(),
                ItemRelease = id != 0 ? itemReleaseRepository.Find(id) : new ItemRelease()
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
            return response;
        }

        public ItemRelease FindItemReleaseById(long id)
        {
            return itemReleaseRepository.Find(id);
        }

        public IEnumerable<ItemRelease> GetAll()
        {
            return itemReleaseRepository.GetAll();
        }

        public IrfHistoryResponse GetIrfHistoryData()
        {
            var irfs = itemReleaseRepository.GetIrfHistoryData();
            var irfList = irfs as IList<ItemRelease> ?? irfs.ToList();
            if (!irfList.Any())
            {
                return new IrfHistoryResponse
                {
                    Irfs = null,
                    IrfItems = new List<ItemReleaseDetail>(),
                    RecentIrf = null
                };
            }
            IrfHistoryResponse response = new IrfHistoryResponse { Irfs = irfList };
            var irfItems = irfList.OrderByDescending(x => x.RecCreatedDate).Select(x => x.ItemReleaseDetails).FirstOrDefault();
            response.IrfItems = irfItems;
            response.RecentIrf = irfList.OrderByDescending(x => x.RecCreatedDate).FirstOrDefault();
            if (response.RecentIrf != null)
            {
                if (!string.IsNullOrEmpty(response.RecentIrf.ManagerId))
                {
                    var manager = aspNetUserRepository.Find(response.RecentIrf.ManagerId).Employee;
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
                itemRelease.Status = releaseStatus.Status;
                itemRelease.ManagerId = releaseStatus.ManagerId;
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
                    if (dbItems.All(x => x.IRFDetailId != itemReleaseDetail.IRFDetailId))
                    {
                        itemReleaseDetail.ItemReleaseId = itemRelease.ItemReleaseId;
                        detailRepository.Add(itemReleaseDetail);
                        detailRepository.SaveChanges();
                        //itemToSave.ItemReleaseDetails.Add(itemReleaseDetail);
                    }
                    if (itemReleaseDetail.IRFDetailId > 0)
                    {
                        itemReleaseDetail.ItemReleaseId = itemRelease.ItemReleaseId;
                        detailRepository.Update(itemReleaseDetail);
                        detailRepository.SaveChanges();
                    }
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
    }
}
