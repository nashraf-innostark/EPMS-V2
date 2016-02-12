using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class AboutUsRepository : BaseRepository<AboutUs>, IAboutUsRepository
    {
        #region Constructor

        public AboutUsRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<AboutUs> DbSet
        {
            get { return db.AboutUs; }
        }

        #endregion

        public AboutUs GetAboutUs()
        {
            return DbSet.FirstOrDefault();
        }

        public AboutUs GetAboutUsForWebsite()
        {
            return DbSet.FirstOrDefault(x=>x.ShowToPublic);
        }

        public AboutUs SearchAboutUs(string search)
        {
            return DbSet.FirstOrDefault(x => (x.ContentAr.Contains(search) || x.ContentEn.Contains(search)));
        }
    }
}
