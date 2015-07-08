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
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

        public PhysicalCountService(IPhysicalCountRepository physicalCountRepository,IWarehouseRepository warehouseRepository,IAspNetUserRepository aspNetUserRepository)
        {
            this.physicalCountRepository = physicalCountRepository;
            this.warehouseRepository = warehouseRepository;
            this.aspNetUserRepository = aspNetUserRepository;
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
