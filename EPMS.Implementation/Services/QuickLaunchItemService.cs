using System;
using System.Collections.Generic;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.MenuModels;
using Microsoft.AspNet.Identity;

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

        public void SaveItems(IEnumerable<int> menuIds)
        {
            if (menuIds != null)
            {
                foreach (int id in menuIds)
                {
                    QuickLaunchItem itemToAdd = new QuickLaunchItem
                    {
                        //UserId = Convert.ToInt64(ClaimsPrincipal.Current.Identity.GetUserId()),
                        UserId = "",
                        MenuId = id,
                    };
                    quickLaunchItemsRepository.Add(itemToAdd);
                    quickLaunchItemsRepository.SaveChanges();
                }
            }
        }
    }
}
