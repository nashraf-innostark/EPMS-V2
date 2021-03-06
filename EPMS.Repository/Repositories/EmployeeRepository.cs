﻿using System;
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
                        { EmployeeByColumn.EmployeeId, c => c.EmployeeId},
                        { EmployeeByColumn.EmployeeFirstName,  c => c.EmployeeFirstName},
                        { EmployeeByColumn.EmployeeMiddleName, c => c.EmployeeMiddleName},
                        { EmployeeByColumn.EmployeeLastName, c => c.EmployeeLastName},
                        { EmployeeByColumn.EmployeeJobId, c => c.EmployeeJobId}
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

            Expression<Func<Employee, bool>> query =
                s => (((employeeSearchRequset.EmployeeId == 0) || s.EmployeeId == employeeSearchRequset.EmployeeId
                    || s.EmployeeId.Equals(employeeSearchRequset.EmployeeId)) &&
                    ((string.IsNullOrEmpty(employeeSearchRequset.EmployeeFirstName) || (s.EmployeeFirstName.Contains(employeeSearchRequset.EmployeeFirstName)))
                    || (s.EmployeeMiddleName.Contains(employeeSearchRequset.EmployeeMiddleName)) || (s.EmployeeLastName.Contains(employeeSearchRequset.EmployeeLastName))) &&
                    ((employeeSearchRequset.EmployeeJobId == "") || (s.EmployeeJobId == employeeSearchRequset.EmployeeJobId)));

            IEnumerable<Employee> employees = employeeSearchRequset.IsAsc ?
                DbSet
                .Where(query).OrderBy(employeeClause[employeeSearchRequset.EmployeeByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(employeeClause[employeeSearchRequset.EmployeeByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new EmployeeResponse { Employeess = employees, TotalCount = DbSet.Count(query) };
        }

        /// <summary>
        /// Find Employee By Employee ID
        /// </summary>
        /// <param name="id">EMployee ID</param>
        /// <returns></returns>
        public Employee FindEmployeeById(long? id)
        {
            return DbSet.FirstOrDefault(employeeId => employeeId.EmployeeId == id);
        }
    }
}
