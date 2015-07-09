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
    public class PhysicalCountService:IPhysicalCountService
    {
        private readonly IPhysicalCountRepository physicalCountRepository;
        private readonly IPhysicalCountItemRepository physicalCountItemRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

        public PhysicalCountService(IPhysicalCountRepository physicalCountRepository,IWarehouseRepository warehouseRepository,IAspNetUserRepository aspNetUserRepository, IPhysicalCountItemRepository physicalCountItemRepository)
        {
            this.physicalCountRepository = physicalCountRepository;
            this.warehouseRepository = warehouseRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.physicalCountItemRepository = physicalCountItemRepository;
        }

        public PhysicalCountResponse GetAllPhysicalCountResponse(PhysicalCountSearchRequest searchRequest)
        {
            return physicalCountRepository.GetAllPhysicalCountResponse(searchRequest);
        }

        public IEnumerable<PhysicalCount> GetAll()
        {
            return physicalCountRepository.GetAll();
        }

        public PhysicalCount FindPhysicalCountById(long id)
        {
            return physicalCountRepository.Find(id);
        }

        public bool SavePhysicalCount(PhysicalCount physicalCount)
        {
            if (physicalCount.PCId > 0)
            {
                if (UpdatePC(physicalCount))
                    PCItemUpdation(physicalCount);
            }
            else
            {
                physicalCountRepository.Add(physicalCount);
                physicalCountRepository.SaveChanges();
            }
            return true;
        }

        public bool UpdatePC(PhysicalCount physicalCount)
        {
            try
            {
                physicalCountRepository.Update(physicalCount);
                physicalCountRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PCItemUpdation(PhysicalCount physicalCount)
        {
            try
            {
                var pcItemsInDb = physicalCountItemRepository.GetPCiItemsByPCiId(physicalCount.PCId).ToList();

                foreach (var physicalCountItem in physicalCount.PhysicalCountItems)
                {
                    if (physicalCountItem.PCItemId > 0)
                    {
                        //update
                        var pcItemInDb = pcItemsInDb.Where(x => x.PCItemId == physicalCountItem.PCItemId);
                        if (pcItemInDb != null)
                        {
                            physicalCountItemRepository.Update(physicalCountItem);
                            physicalCountItemRepository.SaveChanges();
                            pcItemsInDb.RemoveAll(x => x.PCItemId == physicalCountItem.PCItemId);
                        }
                    }
                    else
                    {
                        //save
                        physicalCountItemRepository.Add(physicalCountItem);
                        physicalCountItemRepository.SaveChanges();
                    }
                }
                DeleteRfiItem(pcItemsInDb);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void DeleteRfiItem(IEnumerable<PhysicalCountItem> pcItemsInDb)
        {
            foreach (var physicalCountItem in pcItemsInDb)
            {
                physicalCountItemRepository.Delete(physicalCountItem);
                physicalCountItemRepository.SaveChanges();
            }
        }

        public bool AddPhysicalCount(PhysicalCount physicalCount)
        {
            physicalCountRepository.Add(physicalCount);
            physicalCountRepository.SaveChanges();
            return true;
        }

        public bool UpdatePhysicalCount(PhysicalCount physicalCount)
        {
            physicalCountRepository.Update(physicalCount);
            physicalCountRepository.SaveChanges();
            return true;
        }

        public void DeletePhysicalCount(PhysicalCount physicalCount)
        {
            physicalCountRepository.Delete(physicalCount);
            physicalCountRepository.SaveChanges();
        }

        public PCCreateResponse LoadPhysicalCountResponseData(long? id, string requesterUserId)
        {
            PCCreateResponse createResponse=new PCCreateResponse();
            var employee = new Employee();
            

            createResponse.Warehouses = warehouseRepository.GetAll();
            if (id != null)
            {
                var pc = physicalCountRepository.Find((long) id);
                if (pc != null && pc.PhysicalCountItems.Any())
                {
                    createResponse.PhysicalCount = pc;
                    createResponse.PhysicalCountItems = pc.PhysicalCountItems;

                    createResponse.RequesterEmpId = employee.EmployeeJobId;
                    createResponse.RequesterNameE = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE +
                                                    " " + employee.EmployeeLastNameE;
                    createResponse.RequesterNameA = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA +
                                                    " " + employee.EmployeeLastNameA;
                }
            }
            else
            {
                employee = aspNetUserRepository.Find(requesterUserId).Employee;
                if (employee != null)
                {
                    createResponse.RequesterEmpId = employee.EmployeeJobId;
                    createResponse.RequesterNameE = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE +
                                                    " " + employee.EmployeeLastNameE;
                    createResponse.RequesterNameA = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA +
                                                    " " + employee.EmployeeLastNameA;
                }
            }
            return createResponse;
        }
    }
}
