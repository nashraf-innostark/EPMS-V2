using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    class QuickLaunchItemService : IQuickLaunchItemService
    {
        #region Private

        private readonly IQuickLaunchItemRepository quickLaunchItemsRepository;
        
        #endregion
        #region Constructor
        public QuickLaunchItemService(IQuickLaunchItemRepository quickLaunchItemsRepository)
        {
            this.quickLaunchItemsRepository = quickLaunchItemsRepository;
        }

        #endregion
        public bool AddQuickLaunchItems(QuickLaunchItem quickLaunchItems)
        {
            quickLaunchItemsRepository.Add(quickLaunchItems);
            quickLaunchItemsRepository.SaveChanges();
            return true;
        }

        public void DeletequickLaunchItems(QuickLaunchItem quickLaunchItems)
        {
            quickLaunchItemsRepository.Delete(quickLaunchItems);
            quickLaunchItemsRepository.SaveChanges();
        }

        public IEnumerable<QuickLaunchItem> FindItemsByEmployeeId(long? id)
        {
            return quickLaunchItemsRepository.FindItemsbyEmployeeId(id);
        }

        public void SaveItems(IEnumerable<long> menuIds)
        {

        }
    }
}
