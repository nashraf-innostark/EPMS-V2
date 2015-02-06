using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IDashboardWidgetPreferencesService
    {
        DashboardWidgetPreference LoadPreferencesByUserId(string userId);
        IEnumerable<DashboardWidgetPreference> LoadAllPreferencesByUserId(string userId);
        bool AddPreferences(DashboardWidgetPreference preferences);
        bool UpdatePreferences(DashboardWidgetPreference preferences);
        void Deletepreferences(DashboardWidgetPreference preferences);
    }
}
