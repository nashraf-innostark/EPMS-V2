using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class DashboardWidgetPreferencesMapper
    {
        public static DashboardWidgetPreferences CreateFromClientToServer(this Models.DashboardWidgetPreferences source)
        {
            return new DashboardWidgetPreferences
            {
                WidgetPerferencesId = source.WidgetPerferencesId,
                UserId = source.UserId,
                WidgetId = source.WidgetId,
                SortNumber = source.SortNumber,
            };
        }

        public static Models.DashboardWidgetPreferences CreateFromServerToClient(this DashboardWidgetPreferences source)
        {
            return new Models.DashboardWidgetPreferences
            {
                WidgetPerferencesId = source.WidgetPerferencesId,
                UserId = source.UserId,
                WidgetId = source.WidgetId,
                SortNumber = source.SortNumber,
            };
        }
    }
}