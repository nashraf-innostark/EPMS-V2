using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    class QuickLaunchItemsService : IQuickLaunchItemsService
    {
        #region Private

        private readonly IQuickLaunchItemsRepository quickLaunchItemsRepository;
        
        #endregion
        #region Constructor
        public QuickLaunchItemsService(IQuickLaunchItemsRepository quickLaunchItemsRepository)
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
    }
}
