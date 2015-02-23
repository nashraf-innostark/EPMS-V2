using EPMS.Models.MenuModels;

namespace EPMS.Interfaces.Repository
{
    public interface IMenuRepository : IBaseRepository<Menu, int>
    {
        long GetMenuIdByPermissionKey(string permissionKey);
    }
}
