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
            var direction = Resources.Shared.Common.TextDirection;
            return new QuickLaunchMenuItems
            {
                MenuID = source.Menu.MenuId,
                ImageIconPath = source.Menu.MenuImagePath,
                UrlPath = source.Menu.MenuTargetController + "/" + source.Menu.MenuFunction,
                Name = direction == "ltr" ? source.Menu.MenuTitle : source.Menu.MenuTitleA,
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
            var direction = Resources.Shared.Common.TextDirection;
            retVal.Title = direction == "ltr" ? source.Menu.MenuTitle : source.Menu.MenuTitleA;
            return retVal;
        }
    }
}