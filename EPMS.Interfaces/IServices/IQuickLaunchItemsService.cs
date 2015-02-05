using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IQuickLaunchItemsService
    {
        bool AddQuickLaunchItems(QuickLaunchItems quickLaunchItems);
        void DeletequickLaunchItems(QuickLaunchItems quickLaunchItems);
        /// <summary>
        /// Get all Items by Employee Id
        /// </summary>
        QuickLaunchItems FindItemsByEmployeeId(long? id);
    }
}
