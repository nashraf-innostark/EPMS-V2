using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.MenuModels;


namespace EPMS.Interfaces.IServices
{
    /// <summary>
    /// Interface for Website Menu Service
    /// </summary>
    public interface IWebsiteMenuService
    {       
        WebsiteMenuModel LoadWebsiteMenuItems();
    }
}
