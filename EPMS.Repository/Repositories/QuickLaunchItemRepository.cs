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

        public IEnumerable<QuickLaunchItem> FindItemsbyEmployeeId(long? employeeId)
        {
            return DbSet.Where(s => s.UserId == employeeId).ToList();
        }
    }
}
