﻿using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class RIFService : IRIFService
    {
        private readonly IRIFRepository rifRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IRIFItemRepository rifItemRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rifRepository"></param>
        /// <param name="itemVariationRepository"></param>
        /// <param name="rifItemRepository"></param>
        /// <param name="customerRepository"></param>
        /// <param name="ordersRepository"></param>
        /// <param name="aspNetUserRepository"></param>
        public RIFService(IRIFRepository rifRepository, IItemVariationRepository itemVariationRepository, IRIFItemRepository rifItemRepository, ICustomerRepository customerRepository, IOrdersRepository ordersRepository, IAspNetUserRepository aspNetUserRepository)
        {
            this.rifRepository = rifRepository;
            this.itemVariationRepository = itemVariationRepository;
            this.rifItemRepository = rifItemRepository;
            this.customerRepository = customerRepository;
            this.ordersRepository = ordersRepository;
            this.aspNetUserRepository = aspNetUserRepository;
        }

        #endregion
        public IEnumerable<RIF> GetAll()
        {
            return rifRepository.GetAll();
        }

        public RifRequestResponse LoadAllRifs(RifSearchRequest rifSearchRequest)
        {
            var rifs = rifRepository.LoadAllRifs(rifSearchRequest);
            return rifs;
        }

        public RifHistoryResponse GetRifHistoryData()
        {
            var rifs = rifRepository.GetRifHistoryData();
            
            var rifList = rifs as IList<RIF> ?? rifs.ToList();
            if (!rifList.Any())
            {
                return new RifHistoryResponse
                {
                    Rifs = null,
                    RifItems = new List<RIFItem>(),
                    RecentRif = null
                };
            }

            RifHistoryResponse response = new RifHistoryResponse {Rifs = rifList};
            var rifItems = rifList.OrderByDescending(x => x.RecCreatedDate).Select(x => x.RIFItems).FirstOrDefault();
            response.RifItems = rifItems;
            response.RecentRif = rifList.OrderByDescending(x => x.RecCreatedDate).FirstOrDefault();
            if (response.RecentRif != null)
            {
                if (!string.IsNullOrEmpty(response.RecentRif.ManagerId))
                {
                    var manager = aspNetUserRepository.Find(response.RecentRif.ManagerId).Employee;
                    response.ManagerNameEn = manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " +
                                           manager.EmployeeLastNameE;
                    response.ManagerNameAr = manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " +
                                           manager.EmployeeLastNameA;
                }
                var employee = response.RecentRif.AspNetUser.Employee;
                response.RequesterNameEn = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " +
                                       employee.EmployeeLastNameE;
                response.RequesterNameAr = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " +
                                       employee.EmployeeLastNameA;
            }
            return response;
        }

        public RIF FindRIFById(long id)
        {
            return rifRepository.Find(id);
        }
        public bool SaveRIF(RIF rfi)
        {
            if (rfi.RIFId > 0)
            {
                //update
                if (UpdateRIF(rfi))//if RFI updated, then update the items
                    RifItemUpdation(rfi);
            }
            else
            {
                //save
                AddRIF(rfi);
            }
            return true;
        }

        private void RifItemUpdation(RIF rfi)
        {
            var rfiItemsInDb = rifItemRepository.GetRifItemsByRifId(rfi.RIFId).ToList();

            foreach (var rfiItem in rfi.RIFItems)
            {
                if (rfiItem.RIFItemId > 0)
                {
                    //update
                    var rfiItemInDb = rfiItemsInDb.Where(x => x.RIFItemId == rfiItem.RIFItemId);
                    if (rfiItemInDb != null)
                    {
                        rifItemRepository.Update(rfiItem.CreateRfiItem());
                        rifItemRepository.SaveChanges();
                        rfiItemsInDb.RemoveAll(x => x.RIFItemId == rfiItem.RIFItemId);
                    }
                }
                else
                {
                    //save
                    rifItemRepository.Add(rfiItem);
                    rifItemRepository.SaveChanges();
                }
            }
            DeleteRifItem(rfiItemsInDb);
        }

        private void DeleteRifItem(IEnumerable<RIFItem> rfiItemsInDb)
        {
            foreach (var rfiItem in rfiItemsInDb)
            {
                rifItemRepository.Delete(rfiItem);
                rifItemRepository.SaveChanges();
            }
        }

        public bool AddRIF(RIF rfi)
        {
            rifRepository.Add(rfi);
            rifRepository.SaveChanges();
            return true;
        }

        public bool UpdateRIF(RIF rfi)
        {
            rifRepository.Update(rfi);
            rifRepository.SaveChanges();
            return true;
        }

        public void DeleteRIF(RIF rfi)
        {
            rifRepository.Delete(rfi);
            rifRepository.SaveChanges();
        }

        public RifCreateResponse LoadRifResponseData(long? id, bool loadCustomersAndOrders)
        {
            RifCreateResponse rifResponse = new RifCreateResponse
            {
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList()
            };
            if (id != null)
            {
                var rif = rifRepository.Find((long)id);
                if (rif != null)
                {
                    rifResponse.Rif = rif;
                    var employee = aspNetUserRepository.Find(rif.RecCreatedBy).Employee;
                    rifResponse.RequesterNameE = employee != null ? employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE : "";
                    rifResponse.RequesterNameA = employee != null ? employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA : "";

                    if (rif.Order != null)
                    {
                        rifResponse.OrderNo = rif.Order.OrderNo;
                        var customer = rif.Order.Customer;
                        rifResponse.CustomerNameE = customer != null ? customer.CustomerNameE : "";
                        rifResponse.CustomerNameA = customer != null ? customer.CustomerNameA : "";

                    }

                    if (rif.Status != 6 && !string.IsNullOrEmpty(rif.ManagerId))
                    {
                        var manager = aspNetUserRepository.Find(rif.ManagerId).Employee;
                        rifResponse.ManagerNameE = manager != null ? manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " + manager.EmployeeLastNameE : "";
                        rifResponse.ManagerNameA = manager != null ? manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " + manager.EmployeeLastNameA : "";

                    }
                    rifResponse.RifItem = rif.RIFItems;
                }
            }
            if (loadCustomersAndOrders)
            {
                rifResponse.Customers = customerRepository.GetAll();
                rifResponse.Orders = ordersRepository.GetAll();
            }
            return rifResponse;
        }
    }
}
