﻿namespace EPMS.Web.Models
{
    public class DashboardWidgetPreference
    {
        public long WidgetPerferencesId { get; set; }
        public string UserId { get; set; }
        public string WidgetId { get; set; }
        public int SortNumber { get; set; }
    }
}