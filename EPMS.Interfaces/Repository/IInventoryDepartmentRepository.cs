using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IInventoryDepartmentRepository : IBaseRepository<InventoryDepartment, long>
    {
        bool DepartmentExists(InventoryDepartment department);
    }
}
