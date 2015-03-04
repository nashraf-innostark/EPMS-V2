using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IDashboardWidgetPreferencesRepository : IBaseRepository<DashboardWidgetPreference, long>
    {
        DashboardWidgetPreference LoadPreferencesByUserId(string userId);
        IEnumerable<DashboardWidgetPreference> LoadAllPreferencesByUserId(string userId);
    }
}
