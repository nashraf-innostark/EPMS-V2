using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;
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

        public IEnumerable<Customer> GetCustomerReportList(CustomerReportDetailRequest request)
        {
            var response = DbSet.Include(x=>x.Complaints).Where(x => x.RecCreatedDt >= request.StartDate && x.RecCreatedDt <= request.EndDate);
            return response;
        }

        public Customer GetCustomerByEmployeeId(long employeeId)
        {
            return DbSet.FirstOrDefault(cus => cus.EmployeeId == employeeId);
        }
    }
}
