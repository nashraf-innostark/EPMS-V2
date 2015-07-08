using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IPhysicalCountService
    {
        PhysicalCountResponse GetAllPhysicalCountResponse(PhysicalCountSearchRequest searchRequest);
        IEnumerable<PhysicalCount> GetAll();
        PhysicalCount FindPhysicalCountById(long id);
        bool AddPhysicalCount(PhysicalCount physicalCount);
        bool UpdatePhysicalCount(PhysicalCount physicalCount);
        void DeletePhysicalCount(PhysicalCount physicalCount);
    }
}
