using System.Collections.Generic;
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

        public PhysicalCountService(IPhysicalCountRepository physicalCountRepository)
        {
            this.physicalCountRepository = physicalCountRepository;
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
    }
}
