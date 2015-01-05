using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public sealed class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Employee> DbSet
        {
            get { return db.Employees; }
        }

        #endregion

        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<EmployeeByColumn, Func<Employee, object>> employeeClause =

            new Dictionary<EmployeeByColumn, Func<Employee, object>>
                    {
                        { EmployeeByColumn.EmployeeNameE,  c => c.EmployeeNameE},
                        { EmployeeByColumn.EmployeeJobId, c => c.JobTitleId},
                        { EmployeeByColumn.EmployeeJobTitle, c => c.JobTitle.JobTitleNameE},
                        { EmployeeByColumn.EmployeeDepartment, c => c.JobTitle.Department.DepartmentNameE}
                    };
        #endregion

        /// <summary>
        /// Get All Employees from DB with Fileters if any
        /// </summary>
        /// <param name="employeeSearchRequset">EmployeeSearchRequset</param>
        /// <returns>EmployeeRespone</returns>
        public EmployeeResponse GetAllEmployees(EmployeeSearchRequset employeeSearchRequset)
        {
            int fromRow = (employeeSearchRequset.PageNo - 1) * employeeSearchRequset.PageSize;
            int toRow = employeeSearchRequset.PageSize;
            if (employeeSearchRequset.SortBy == 1)
            {
                employeeSearchRequset.SortBy = 2;
            }
            Expression<Func<Employee, bool>> query =
                s => ((string.IsNullOrEmpty(employeeSearchRequset.SearchStr)) || (s.EmployeeNameE.Contains(employeeSearchRequset.SearchStr)) || 
                    (s.EmployeeNameA.Contains(employeeSearchRequset.SearchStr)) || (s.JobTitle.JobTitleNameE.Contains(employeeSearchRequset.SearchStr)) || 
                    (s.JobTitle.JobTitleNameA.Contains(employeeSearchRequset.SearchStr)) || (s.EmployeeJobId == employeeSearchRequset.SearchStr) || 
                    (s.JobTitle.Department.DepartmentNameE.Contains(employeeSearchRequset.SearchStr)) || (s.JobTitle.Department.DepartmentNameA.Contains(employeeSearchRequset.SearchStr)));

            IEnumerable<Employee> employees = employeeSearchRequset.IsAsc ?
                DbSet
                .Where(query).OrderBy(employeeClause[employeeSearchRequset.EmployeeByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(employeeClause[employeeSearchRequset.EmployeeByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new EmployeeResponse { Employeess = employees, TotalDisplayRecords = DbSet.Count(query), TotalRecords = DbSet.Count() };
        }

        /// <summary>
        /// Get all employees in a depertment
        /// </summary>
        public IEnumerable<Employee> GetEmployeesByDepartmentId(long departmentId)
        {
            return DbSet.Where(employee => employee.JobTitle.DepartmentId == departmentId);
        }
        public Employee FindForPayroll(long employeeId, DateTime currTime)
        {
            return DbSet.FirstOrDefault(employee => employee.EmployeeId == employeeId && employee.Allowances.Count(y=>y.AllowanceDate <= currTime)>0);
        }
    }
}
