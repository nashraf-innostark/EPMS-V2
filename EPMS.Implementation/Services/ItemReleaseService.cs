using System;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class ItemReleaseService : IItemReleaseService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IItemReleaseRepository itemReleaseRepository;

        public ItemReleaseService(ICustomerRepository customerRepository, IItemVariationRepository itemVariationRepository, IItemReleaseRepository itemReleaseRepository)
        {
            this.customerRepository = customerRepository;
            this.itemVariationRepository = itemVariationRepository;
            this.itemReleaseRepository = itemReleaseRepository;
        }

        public IRFCreateResponse GetCreateResponse(long id)
        {
            IRFCreateResponse response = new IRFCreateResponse
            {
                Customers = customerRepository.GetAll(),
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList().ToList(),
                ItemRelease = id != 0 ? itemReleaseRepository.Find(id) : new ItemRelease()
            };
            return response;
        }
    }
}
