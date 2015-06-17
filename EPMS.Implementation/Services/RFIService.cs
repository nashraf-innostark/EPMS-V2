using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using FaceSharp.Api.Extensions;

namespace EPMS.Implementation.Services
{
    public class RFIService : IRFIService
    {
        private readonly IRFIRepository rfiRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IRFIItemRepository rfiItemRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly IRFIHistoryRepository historyRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rfiRepository"></param>
        /// <param name="itemVariationRepository"></param>
        /// <param name="rfiItemRepository"></param>
        /// <param name="customerRepository"></param>
        /// <param name="ordersRepository"></param>
        /// <param name="aspNetUserRepository"></param>
        public RFIService(IRFIRepository rfiRepository, IItemVariationRepository itemVariationRepository, IRFIItemRepository rfiItemRepository, ICustomerRepository customerRepository, IOrdersRepository ordersRepository, IAspNetUserRepository aspNetUserRepository, IRFIHistoryRepository historyRepository)
        {
            this.rfiRepository = rfiRepository;
            this.itemVariationRepository = itemVariationRepository;
            this.rfiItemRepository = rfiItemRepository;
            this.customerRepository = customerRepository;
            this.ordersRepository = ordersRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.historyRepository = historyRepository;
        }

        #endregion
        public IEnumerable<RFI> GetAll()
        {
            return rfiRepository.GetAll();
        }

        public RfiRequestResponse LoadAllRfis(RfiSearchRequest rfiSearchRequest)
        {
            var rfis = rfiRepository.LoadAllRfis(rfiSearchRequest);
            return rfis;
        }

        public RfiHistoryResponse GetRfiHistoryData(long? parentId)
        {
            if (parentId == null)
            {
                return new RfiHistoryResponse();
            }
            var rfis = historyRepository.GetRfiHistoryData((long)parentId);
            if (rfis == null)
            {
                return new RfiHistoryResponse
                {
                    Rfis = null,
                    RfiItems = new List<RFIItem>(),
                    RecentRfi = null
                };
            }
            var rfiList = rfis as IList<RFIHistory> ?? rfis.ToList();
            RfiHistoryResponse response = new RfiHistoryResponse
            {
                Rfis = rfiList.Select(x => x.CreateFromRfiHistoryToRfi()),
                RecentRfi = rfiRepository.Find((long)parentId)
            };
            response.RfiItems = response.RecentRfi.RFIItems;

            if (response.RecentRfi != null)
            {
                if (!string.IsNullOrEmpty(response.RecentRfi.ManagerId))
                {
                    var manager = aspNetUserRepository.Find(response.RecentRfi.ManagerId).Employee;
                    response.ManagerNameEn = manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " +
                                           manager.EmployeeLastNameE;
                    response.ManagerNameAr = manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " +
                                           manager.EmployeeLastNameA;
                }
                if (response.RecentRfi.AspNetUser != null)
                {
                    var employee = response.RecentRfi.AspNetUser.Employee;
                    response.RequesterNameEn = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " +
                                           employee.EmployeeLastNameE;
                    response.RequesterNameAr = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " +
                                           employee.EmployeeLastNameA;
                }
            }
            return response;
        }

        public RFI FindRFIById(long id)
        {
            return rfiRepository.Find(id);
        }
        public bool SaveRFI(RFI rfi)
        {
            if (rfi.RFIId > 0)
            {
                //update
                if (UpdateRFI(rfi))//if RFI updated, then update the items
                    RfiItemUpdation(rfi);
            }
            else
            {
                //save
                AddRFI(rfi);
            }
            return true;
        }

        private void RfiItemUpdation(RFI rfi)
        {
            var rfiItemsInDb = rfiItemRepository.GetRfiItemsByRfiId(rfi.RFIId).ToList();

            foreach (var rfiItem in rfi.RFIItems)
            {
                if (rfiItem.RFIItemId > 0)
                {
                    //update
                    var rfiItemInDb = rfiItemsInDb.Where(x => x.RFIItemId == rfiItem.RFIItemId);
                    if (rfiItemInDb != null)
                    {
                        rfiItemRepository.Update(rfiItem.CreateRfiItem());
                        rfiItemRepository.SaveChanges();
                        rfiItemsInDb.RemoveAll(x => x.RFIItemId == rfiItem.RFIItemId);
                    }
                }
                else
                {
                    //save
                    rfiItemRepository.Add(rfiItem);
                    rfiItemRepository.SaveChanges();
                }
            }
            DeleteRfiItem(rfiItemsInDb);
        }

        private void DeleteRfiItem(IEnumerable<RFIItem> rfiItemsInDb)
        {
            foreach (var rfiItem in rfiItemsInDb)
            {
                rfiItemRepository.Delete(rfiItem);
                rfiItemRepository.SaveChanges();
            }
        }

        public bool AddRFI(RFI rfi)
        {
            rfiRepository.Add(rfi);
            rfiRepository.SaveChanges();
            return true;
        }

        public bool UpdateRFI(RFI rfi)
        {
            var previous = rfiRepository.Find(rfi.RFIId);
            if (previous.Status != rfi.Status && rfi.Status != 2)
            {
                var rfiHistoryToAdd = rfi.CreateFromRfiToRfiHistory(previous.RFIItems);
                historyRepository.Add(rfiHistoryToAdd);
                historyRepository.SaveChanges();
            }
            rfiRepository.Update(rfi);
            rfiRepository.SaveChanges();
            return true;
        }

        public void DeleteRFI(RFI rfi)
        {
            rfiRepository.Delete(rfi);
            rfiRepository.SaveChanges();
        }

        public RFICreateResponse LoadRfiResponseData(long? id, bool loadCustomersAndOrders, string from)
        {
            RFICreateResponse rfiResponse = new RFICreateResponse
            {
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList()
            };
            if (id != null)
            {
                RFI rfi;
                if (from == "History")
                {
                    rfi = historyRepository.Find((long)id).CreateFromRfiHistoryToRfiBase();
                }
                else
                {
                    rfi = rfiRepository.Find((long)id);
                }
                
                if (rfi != null)
                {
                    rfiResponse.Rfi = rfi;
                    var employee = aspNetUserRepository.Find(rfi.RecCreatedBy).Employee;
                    rfiResponse.RequesterNameE = employee != null ? employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE : "";
                    rfiResponse.RequesterNameA = employee != null ? employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA : "";

                    if (rfi.Order != null)
                    {
                        rfiResponse.OrderNo = rfi.Order.OrderNo;
                        var customer = rfi.Order.Customer;
                        rfiResponse.CustomerNameE = customer != null ? customer.CustomerNameE : "";
                        rfiResponse.CustomerNameA = customer != null ? customer.CustomerNameA : "";

                    }

                    if (rfi.Status != 6 && !string.IsNullOrEmpty(rfi.ManagerId))
                    {
                        var manager = aspNetUserRepository.Find(rfi.ManagerId).Employee;
                        rfiResponse.ManagerNameE = manager != null ? manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " + manager.EmployeeLastNameE : "";
                        rfiResponse.ManagerNameA = manager != null ? manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " + manager.EmployeeLastNameA : "";

                    }
                    rfiResponse.RfiItem = rfi.RFIItems;
                }
            }
            if (loadCustomersAndOrders)
            {
                rfiResponse.Customers = customerRepository.GetAll();
                rfiResponse.Orders = ordersRepository.GetAll();
            }
            return rfiResponse;
        }

        public IEnumerable<RFI> GetCustomerRfis(long customerId)
        {
            //IEnumerable<Order> customerOrders = ordersRepository.GetOrdersByCustomerId(customerId);
            //IEnumerable<RFI> customerRfis = customerOrders.Select(x => x.RFIs.Select(x=>x.));
            return null;
        }
    }
}
