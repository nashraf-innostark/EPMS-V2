using System;
using System.Collections.Generic;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.MenuModels;
using FaceSharp.Api.Objects;
using Microsoft.AspNet.Identity;

namespace EPMS.Implementation.Services
{
    internal class QuickLaunchItemService : IQuickLaunchItemService
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

        /// <summary>
        /// Find Items by Employee Id for Quick Launch User Items
        /// </summary>
        public IEnumerable<QuickLaunchItem> FindItemsByEmployeeId(string id)
        {
            return quickLaunchItemsRepository.FindItemsbyEmployeeId(id);
        }

        /// <summary>
        /// Find Items by User and Menu Id for Deletion
        /// </summary>
        public QuickLaunchItem FindItemsByUserAndMenuId(string userId, int menuId)
        {
            return quickLaunchItemsRepository.GetItemByUserAndMenuId(userId, menuId);
        }

        /// <summary>
        /// Delete Quick Launch User Item
        /// </summary>
        public bool DeleteItem(string userId, int menuId)
        {
            QuickLaunchItem itemToDelete = quickLaunchItemsRepository.GetItemByUserAndMenuId(userId, menuId);
            if (itemToDelete != null)
            {
                quickLaunchItemsRepository.Delete(itemToDelete);
                quickLaunchItemsRepository.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Save Quick Launch User Item
        /// </summary>
        public void SaveItems(IEnumerable<int> menuIds)
        {
            if (menuIds != null)
            {
                var maxOrder = quickLaunchItemsRepository.GetMaxSortOrder(ClaimsPrincipal.Current.Identity.GetUserId());
                foreach (int id in menuIds)
                {
                    QuickLaunchItem itemToAdd = new QuickLaunchItem
                    {
                        UserId = ClaimsPrincipal.Current.Identity.GetUserId(),
                        MenuId = id,
                        SortOrder = maxOrder + 1,
                    };
                    quickLaunchItemsRepository.Add(itemToAdd);
                    quickLaunchItemsRepository.SaveChanges();
                    maxOrder++;
                }
            }
        }

        public void SaveItemPrefrences(string userId, int[] preferences)
        {
            var itemsToDelete = quickLaunchItemsRepository.FindItemsbyEmployeeId(userId);
            foreach (QuickLaunchItem item in itemsToDelete)
            {
                quickLaunchItemsRepository.Delete(item);
                quickLaunchItemsRepository.SaveChanges();
            }
            var maxOrder = quickLaunchItemsRepository.GetMaxSortOrder(ClaimsPrincipal.Current.Identity.GetUserId());
            foreach (int id in preferences)
            {
                QuickLaunchItem itemToAdd = new QuickLaunchItem
                {
                    UserId = ClaimsPrincipal.Current.Identity.GetUserId(),
                    MenuId = id,
                    SortOrder = maxOrder + 1,
                };
                quickLaunchItemsRepository.Add(itemToAdd);
                quickLaunchItemsRepository.SaveChanges();
                maxOrder++;
            }
        }

        public QuickLaunchItem GetItemByUserAndMenuId(string userId, int menuId)
        {
            return quickLaunchItemsRepository.GetItemByUserAndMenuId(userId, menuId);
        }
    }
}