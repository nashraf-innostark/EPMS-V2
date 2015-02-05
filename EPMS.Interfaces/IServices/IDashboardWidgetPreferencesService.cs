using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IDashboardWidgetPreferencesService
    {
        DashboardWidgetPreferences LoadPreferencesByUserId(string userId);
        IEnumerable<DashboardWidgetPreferences> LoadAllPreferencesByUserId(string userId);
        bool Addpreferences(DashboardWidgetPreferences preferences);
        bool Updatepreferences(DashboardWidgetPreferences preferences);
        void Deletepreferences(DashboardWidgetPreferences preferences);
    }
}
