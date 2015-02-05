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
        IEnumerable<QuickLaunchItem> FindItemsByEmployeeId(long? id);
    }
}
