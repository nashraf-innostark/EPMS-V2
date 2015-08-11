using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class DashboardWidgetPreferencesMapper
    {
        public static DashboardWidgetPreference CreateFromClientToServerWidgetPreferences(this WebsiteModels.DashboardWidgetPreference source)
        {
            return new DashboardWidgetPreference
            {
                WidgetPerferencesId = source.WidgetPerferencesId,
                UserId = source.UserId,
                WidgetId = source.WidgetId,
                SortNumber = source.SortNumber,
            };
        }

        public static WebsiteModels.DashboardWidgetPreference CreateFromClientToServerWidgetPreferences(this DashboardWidgetPreference source)
        {
            return new WebsiteModels.DashboardWidgetPreference
            {
                WidgetPerferencesId = source.WidgetPerferencesId,
                UserId = source.UserId,
                WidgetId = source.WidgetId,
                SortNumber = source.SortNumber,
            };
        }
    }
}