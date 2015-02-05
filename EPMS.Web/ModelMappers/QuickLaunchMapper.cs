using EPMS.Models.MenuModels;
using EPMS.Web.Models;

namespace EPMS.Web.ModelMappers
{
    /// <summary>
    /// Quick Launch Mapper
    /// </summary>
    public static class QuickLaunchMapper
    {
        /// <summary>
        /// Create Quick Launch Item from Menu
        /// </summary>
        public static QuickLaunchItem CreateFrom(this MenuRight source)
        {
            return new QuickLaunchItem
            {
                MenuID = source.Menu.MenuId,
                ImageIconPath = source.Menu.MenuImagePath,
                UrlPath = source.Menu.MenuTargetController + "/" + source.Menu.MenuFunction,
                Name = source.Menu.MenuTitle
            };
        }
    }
}