using System.Collections.Generic;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IQuickLaunchItemRepository : IBaseRepository<QuickLaunchItem, long>
    {
        IEnumerable<QuickLaunchItem> FindItemsbyEmployeeId(string employeeId);
        QuickLaunchItem GetItemByUserAndMenuId(string userId, int menuId);
        int GetMaxSortOrder(string userId);
        
    }
}
