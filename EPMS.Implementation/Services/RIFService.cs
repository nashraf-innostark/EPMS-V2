using System;
using System.Collections.Generic;
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
        private readonly IRIFHistoryRepository historyRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IRIFItemRepository rifItemRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IEmployeeRepository employeeRepository;

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
        public RIFService(IRIFRepository rifRepository, IItemVariationRepository itemVariationRepository, IRIFItemRepository rifItemRepository, ICustomerRepository customerRepository, IOrdersRepository ordersRepository, IAspNetUserRepository aspNetUserRepository, IRIFHistoryRepository historyRepository, IWarehouseRepository warehouseRepository, IEmployeeRepository employeeRepository)
        {
            this.rifRepository = rifRepository;
            this.itemVariationRepository = itemVariationRepository;
            this.rifItemRepository = rifItemRepository;
            this.customerRepository = customerRepository;
            this.ordersRepository = ordersRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.historyRepository = historyRepository;
            this.warehouseRepository = warehouseRepository;
            this.employeeRepository = employeeRepository;
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

        public RifHistoryResponse GetRifHistoryData(long? parentId)
        {
            if (parentId == null)
            {
                return new RifHistoryResponse();
            }
            var rifs = historyRepository.GetRifHistoryData((long)parentId);
            var rifList = rifs as IList<RIFHistory> ?? rifs.ToList();

            RifHistoryResponse response = new RifHistoryResponse
            {
                Rifs = rifList.Select(x => x.CreateFromRifHistoryToRif()),
                RecentRif = rifRepository.Find((long) parentId)
            };
            response.RifItems = response.RecentRif.RIFItems;
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
                if (response.RecentRif.AspNetUser != null)
                {
                    var employee = response.RecentRif.AspNetUser.Employee;
                    response.RequesterNameEn = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " +
                                           employee.EmployeeLastNameE;
                    response.RequesterNameAr = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " +
                                           employee.EmployeeLastNameA;
                }
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

        public bool UpdateRIF(RIF rif)
        {
            var previousRif = rifRepository.Find(rif.RIFId);
            if (previousRif.Status != rif.Status && rif.Status != 6)
            {
                var rifHistoryToAdd = rif.CreateFromRifToRifHistory(previousRif.Order,previousRif.RIFItems);
                historyRepository.Add(rifHistoryToAdd);
                historyRepository.SaveChanges();
            }
            rifRepository.Update(rif);
            rifRepository.SaveChanges();
            return true;
        }

        public void DeleteRIF(RIF rfi)
        {
            rifRepository.Delete(rfi);
            rifRepository.SaveChanges();
        }

        public RifCreateResponse LoadRifResponseData(long? id, bool loadCustomersAndOrders, string from)
        {
            RifCreateResponse rifResponse = new RifCreateResponse
            {
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList(),
                LastFormNumber = rifRepository.GetLastFormNumber(),
                Warehouses = warehouseRepository.GetAll(),
            };
            if (id != null)
            {
                RIF rif;
                if (from == "History")
                {
                    rif = historyRepository.Find((long)id).CreateFromRifHistoryToRifWithItems();
                }
                else
                {
                    rif = rifRepository.Find((long)id);
                }
                if (rif != null)
                {
                    rifResponse.Rif = rif;
                    var employee = aspNetUserRepository.Find(rif.RecCreatedBy).Employee;
                    if (employee != null)
                    {
                        rifResponse.RequesterNameE = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE ;
                        rifResponse.RequesterNameA = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA ;
                        rifResponse.EmpJobId = employee.EmployeeJobId;
                    }
                   
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

        public IEnumerable<RIF> GetRecentRIFs(int status, string requester, DateTime date)
        {
            return rifRepository.GetRecentRIFs(status, requester, date);
        }
    }
}
