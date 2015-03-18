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

        #region Public

        public Allowance FindAllownce(long employeeId, DateTime currTime)
        {
            var allowance = DbSet.OrderByDescending(a => a.AllowanceDate).FirstOrDefault(allow => (allow.Employee.EmployeeId == employeeId) && (allow.AllowanceDate.Value.Month <= currTime.Month && allow.AllowanceDate.Value.Year <= currTime.Year));
            return allowance;
        }
        public IEnumerable<Allowance> FindAllownceFromTo(long employeeId, DateTime from, DateTime to)
        {
            var allowance = DbSet.Where(x => x.EmployeeId == employeeId && x.AllowanceDate >= from && x.AllowanceDate <= to);
            return allowance;
        }

        public Allowance FindLastAllownce(long employeeId)
        {
            return DbSet.Where(x=>x.EmployeeId == employeeId).OrderByDescending(x => x.AllowanceDate).FirstOrDefault();
        }

        #endregion
    }
}