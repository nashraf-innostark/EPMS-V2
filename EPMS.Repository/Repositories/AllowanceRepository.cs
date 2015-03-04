using System;
using System.Data.Entity;
using System.Linq;
using EPMS.Repository.BaseRepository;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public sealed class AllowanceRepository : BaseRepository<Allowance>, IAllowanceRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AllowanceRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Allowance> DbSet
        {
            get { return db.Allowances; }
        }

        #endregion

        #region Public

        public Allowance FindForAllownce(long employeeId, DateTime currTime)
        {
            var allowance = DbSet.OrderByDescending(a => a.AllowanceDate).FirstOrDefault(allow => (allow.Employee.EmployeeId == employeeId) && (allow.AllowanceDate <= currTime));
            return allowance;
        }

        #endregion


    }
}
