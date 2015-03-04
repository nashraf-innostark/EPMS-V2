using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IQuickLaunchItemService
    {
        bool AddQuickLaunchItems(QuickLaunchItem quickLaunchItems);
        void DeletequickLaunchItems(QuickLaunchItem quickLaunchItems);
        /// <summary>
        /// Get all Items by Employee Id
        /// </summary>
        IEnumerable<QuickLaunchItem> FindItemsByEmployeeId(string id);
        bool DeleteItem(string userId, int menuId);

        void SaveItems(IEnumerable<int> menuIds);
        void SaveItemPrefrences(string userId, int[] preferences);

        QuickLaunchItem GetItemByUserAndMenuId(string userId, int menuId);
    }
}
