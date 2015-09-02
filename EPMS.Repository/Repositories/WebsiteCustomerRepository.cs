using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class WebsiteCustomerRepository : BaseRepository<WebsiteCustomer>, IWebsiteCustomerRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WebsiteCustomerRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<WebsiteCustomer> DbSet
        {
            get { return db.WebsiteCustomers; }
        }

        #endregion
    }
}
