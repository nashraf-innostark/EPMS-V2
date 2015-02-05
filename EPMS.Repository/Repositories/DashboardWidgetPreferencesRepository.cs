using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class DashboardWidgetPreferencesRepository : BaseRepository<DashboardWidgetPreferences>, IDashboardWidgetPreferencesRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardWidgetPreferencesRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<DashboardWidgetPreferences> DbSet
        {
            get { return db.Preferenceses; }
        }

        #endregion

        public DashboardWidgetPreferences LoadPreferencesByUserId(string userId)
        {
            return DbSet.FirstOrDefault(pref => pref.UserId == userId);
        }

        public IEnumerable<DashboardWidgetPreferences> LoadAllPreferencesByUserId(string userId)
        {
            return DbSet.Where(pref => pref.UserId == userId).ToList();
        }
    }
}
