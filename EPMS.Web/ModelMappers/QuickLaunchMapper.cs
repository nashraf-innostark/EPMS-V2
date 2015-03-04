using EPMS.Models.DomainModels;
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
        public static QuickLaunchMenuItems CreateFrom(this MenuRight source)
        {
            return new QuickLaunchMenuItems
            {
                MenuID = source.Menu.MenuId,
                ImageIconPath = source.Menu.MenuImagePath,
                UrlPath = source.Menu.MenuTargetController + "/" + source.Menu.MenuFunction,
                Name = source.Menu.MenuTitle
            };
        }
        public static QuickLaunchItem CreateFromClientToServer(this QuickLaunchItem source)
        {
            return new QuickLaunchItem
            {
                UserId = source.UserId,
                MenuId = source.MenuId,
                SortOrder = source.SortOrder,
            };
        }

        public static QuickLaunchUserItems CreateForUserItems(this QuickLaunchItem source)
        {
            QuickLaunchUserItems retVal = new QuickLaunchUserItems();
            retVal.MenuId = source.MenuId;
            retVal.UserId = source.UserId;
            retVal.Url = source.Menu.MenuTargetController + "/" + source.Menu.MenuFunction;
            retVal.ImagePath = source.Menu.MenuImagePath;
            retVal.Title = source.Menu.MenuTitle;
            return retVal;
        }
    }
}