using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class DashboardWidgetPreferencesRepository : BaseRepository<DashboardWidgetPreference>, IDashboardWidgetPreferencesRepository
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
        protected override IDbSet<DashboardWidgetPreference> DbSet
        {
            get { return db.Preferences; }
        }

        #endregion

        public DashboardWidgetPreference LoadPreferencesByUserId(string userId)
        {
            return DbSet.FirstOrDefault(pref => pref.UserId == userId);
        }

        public IEnumerable<DashboardWidgetPreference> LoadAllPreferencesByUserId(string userId)
        {
            return DbSet.Where(pref => pref.UserId == userId).ToList();
        }
    }
}
