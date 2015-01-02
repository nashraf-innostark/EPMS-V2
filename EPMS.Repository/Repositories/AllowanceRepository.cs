using System;
using System.Collections.Generic;
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

        public IEnumerable<Allowance> FindForPayroll(long employeeId, DateTime currTime)
        {
            return DbSet.Where(allo => allo.EmployeeId == employeeId && (allo.AllowanceDate.Value.Month == currTime.Month && allo.AllowanceDate.Value.Year == currTime.Year));
        }
    }
}
