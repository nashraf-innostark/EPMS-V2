using System;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository repository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

        public PurchaseOrderService(IItemVariationRepository itemVariationRepository, IPurchaseOrderRepository repository, IAspNetUserRepository aspNetUserRepository)
        {
            this.itemVariationRepository = itemVariationRepository;
            this.repository = repository;
            this.aspNetUserRepository = aspNetUserRepository;
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

            var employee = aspNetUserRepository.Find(po.RecCreatedBy).Employee;
            response.RequesterNameE = employee != null ? employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE : "";
            response.RequesterNameA = employee != null ? employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA : "";
            
            return response;
        }
    }
}
