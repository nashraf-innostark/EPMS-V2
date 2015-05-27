using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IInventoryDepartmentRepository : IBaseRepository<InventoryDepartment, long>
    {
        bool DepartmentExists(InventoryDepartment department);
        IEnumerable<InventoryDepartment> GetAllDepartments();
    }
}
