using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class QuickLaunchItemRepository : BaseRepository<QuickLaunchItem>, IQuickLaunchItemRepository
    {
        public QuickLaunchItemRepository(IUnityContainer container) 
            : base(container)
        {
        }

        protected override IDbSet<QuickLaunchItem> DbSet
        {
            get { return db.QuickLaunchItems; }
        }

        public IEnumerable<QuickLaunchItem> FindItemsbyEmployeeId(string employeeId)
        {
            return DbSet.Include(m=>m.Menu).Where(s => s.UserId == employeeId).ToList().OrderBy(x => x.SortOrder);
        }

        public QuickLaunchItem GetItemByUserAndMenuId(string userId, int menuId)
        {
            return DbSet.FirstOrDefault(item => item.UserId == userId && item.MenuId == menuId);
        }

        public int GetMaxSortOrder(string userId)
        {
            var quickLaunchItem = DbSet.Where(item => item.UserId == userId).OrderByDescending(x => x.SortOrder).FirstOrDefault();
            if (quickLaunchItem != null)
            {
                return quickLaunchItem.SortOrder;
            }
            return 0;
        }
    }
}
