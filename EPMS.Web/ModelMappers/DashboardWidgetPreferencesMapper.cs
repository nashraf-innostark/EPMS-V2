using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class DashboardWidgetPreferencesMapper
    {
        public static DashboardWidgetPreference CreateFromClientToServerWidgetPreferences(this Models.DashboardWidgetPreference source)
        {
            return new DashboardWidgetPreference
            {
                WidgetPerferencesId = source.WidgetPerferencesId,
                UserId = source.UserId,
                WidgetId = source.WidgetId,
                SortNumber = source.SortNumber,
            };
        }

        public static Models.DashboardWidgetPreference CreateFromClientToServerWidgetPreferences(this DashboardWidgetPreference source)
        {
            return new Models.DashboardWidgetPreference
            {
                WidgetPerferencesId = source.WidgetPerferencesId,
                UserId = source.UserId,
                WidgetId = source.WidgetId,
                SortNumber = source.SortNumber,
            };
        }
    }
}