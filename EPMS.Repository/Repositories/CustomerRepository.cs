using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public sealed class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerRepository(IUnityContainer container)
            : base(container)
        {
        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Customer> DbSet
        {
            get { return db.Customers; }
        }
    }
}
