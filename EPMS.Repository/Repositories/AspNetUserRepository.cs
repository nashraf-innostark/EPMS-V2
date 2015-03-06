using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class AspNetUserRepository : BaseRepository<AspNetUser>, IAspNetUserRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AspNetUserRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AspNetUser> DbSet
        {
            get { return db.AspNetUsers; }
        }

        #endregion

        public string GetUserIdByEmployeeId(long employeeId)
        {
            var user = DbSet.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (user != null)
                return user.Id;
            return "";
        }

        public string GetUserIdByCustomerId(long customerId)
        {
            var user = DbSet.FirstOrDefault(x => x.CustomerId == customerId);
            if (user != null)
                return user.Id;
            return "";
        }

        public IEnumerable<AspNetUser> GetAdminUsers(long menuId)
        {
            return DbSet.Where(y => y.AspNetRoles.Any(z => z.MenuRights.Any(a => a.Menu_MenuId == menuId)));
        }
    }
}
