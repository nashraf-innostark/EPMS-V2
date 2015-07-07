using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IPhysicalCountService
    {
        IEnumerable<PhysicalCount> GetAll();
        PhysicalCount FindPhysicalCountById(long id);
        bool AddPhysicalCount(PhysicalCount physicalCount);
        bool UpdatePhysicalCount(PhysicalCount physicalCount);
        void DeletePhysicalCount(PhysicalCount physicalCount);
    }
}
