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
    public class RFIService:IRFIService
    {
        private readonly IRFIRepository rfiRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IRFIItemRepository rfiItemRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

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
        public RFIService(IRFIRepository rfiRepository,IItemVariationRepository itemVariationRepository,IRFIItemRepository rfiItemRepository,ICustomerRepository customerRepository,IOrdersRepository ordersRepository, IAspNetUserRepository aspNetUserRepository)
        {
            this.rfiRepository = rfiRepository;
            this.itemVariationRepository = itemVariationRepository;
            this.rfiItemRepository = rfiItemRepository;
            this.customerRepository = customerRepository;
            this.ordersRepository = ordersRepository;
            this.aspNetUserRepository = aspNetUserRepository;
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

        public RFI FindRFIById(long id)
        {
            return rfiRepository.Find(id);
        }
        public bool SaveRFI(RFI rfi)
        {
            if (rfi.RFIId > 0)
            {
                //update
                if(UpdateRFI(rfi))//if RFI updated, then update the items
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
            rfiRepository.Update(rfi);
            rfiRepository.SaveChanges();
            return true;
        }

        public void DeleteRFI(RFI rfi)
        {
            rfiRepository.Delete(rfi);
            rfiRepository.SaveChanges();
        }

        public RFICreateResponse LoadRfiResponseData(long? id, bool loadCustomersAndOrders)
        {
            RFICreateResponse rfiResponse=new RFICreateResponse
            {
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList()
            };
            if (id != null)
            {
                var rfi = rfiRepository.Find((long)id);
                if (rfi != null)
                {
                    rfiResponse.Rfi = rfi;
                    var employee = aspNetUserRepository.Find(rfi.RecCreatedBy).Employee;
                    rfiResponse.RequesterNameE = employee!=null?employee.EmployeeFirstNameE+" "+employee.EmployeeMiddleNameE+" "+employee.EmployeeLastNameE:"";
                    rfiResponse.RequesterNameA = employee != null ? employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA : "";

                    if (rfi.Order != null)
                    {
                        rfiResponse.OrderNo = rfi.Order.OrderNo;
                        var customer = rfi.Order.Customer;
                        rfiResponse.CustomerNameE = customer != null ? customer.CustomerNameE:"";
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
    }
}
