using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class UserPrefrencesRepository : BaseRepository<UserPrefrence>, IUserPrefrencesRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public UserPrefrencesRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<UserPrefrence> DbSet
        {
            get { return db.UserPrefrence; }
        }

        #endregion

        public UserPrefrence GetPrefrencesByUserId(string userId)
        {
            return DbSet.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
