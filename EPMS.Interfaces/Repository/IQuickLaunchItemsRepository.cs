using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IQuickLaunchItemsRepository : IBaseRepository<QuickLaunchItems, long>
    {
        IEnumerable<QuickLaunchItems> FindItemsbyEmployeeId(long? employeeId);
    }
}
