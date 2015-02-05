using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IDashboardWidgetPreferencesRepository : IBaseRepository<DashboardWidgetPreferences, long>
    {
        DashboardWidgetPreferences LoadPreferencesByUserId(string userId);
        IEnumerable<DashboardWidgetPreferences> LoadAllPreferencesByUserId(string userId);
    }
}
