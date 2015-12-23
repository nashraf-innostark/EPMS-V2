using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ContactUsRepository : BaseRepository<ContactUs>, IContactUsRepository
    {
        #region Constructor
        
        public ContactUsRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<ContactUs> DbSet
        {
            get { return db.ContactUs; }
        }

        #endregion

        public ContactUs GetContactUs()
        {
            return DbSet.FirstOrDefault(x=>x.ShowToPublic);
        }

        public ContactUs SearchContactUs(string search)
        {
            return DbSet.FirstOrDefault(x => (x.ContentAr.Contains(search) || x.ContentEn.Contains(search)));
        }
    }
}
