using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.MenuModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MenuRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Menu> DbSet
        {
            get { return db.Menus; }
        }
        #endregion

        public long GetMenuIdByPermissionKey(string permissionKey)
        {
            var menu = DbSet.SingleOrDefault(x => x.PermissionKey == permissionKey);
            if (menu != null)
            {
                var id = menu.MenuId;
                return id;
            }
            return 0;
        }
    }
}