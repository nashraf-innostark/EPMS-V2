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

        public ItemReleaseService(ICustomerRepository customerRepository, IItemVariationRepository itemVariationRepository, IItemReleaseRepository itemReleaseRepository, IRFIRepository rfiRepository, IOrdersRepository ordersRepository, IItemReleaseDetailRepository detailRepository)
        {
            this.customerRepository = customerRepository;
            this.itemVariationRepository = itemVariationRepository;
            this.itemReleaseRepository = itemReleaseRepository;
            this.ordersRepository = ordersRepository;
            this.detailRepository = detailRepository;
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
