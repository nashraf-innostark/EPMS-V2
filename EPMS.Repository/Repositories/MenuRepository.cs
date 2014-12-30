using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.MenuModels;
using Microsoft.Practices.Unity;
using EPMS.Repository.BaseRepository;

namespace EPMS.Web.Views.RolesAdmin
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
    }
}