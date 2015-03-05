﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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
                        { EmployeeByColumn.EmployeeNameE,  c => c.EmployeeFirstNameE},
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
            int fromRow = employeeSearchRequset.iDisplayStart;
            int toRow = employeeSearchRequset.iDisplayStart + employeeSearchRequset.iDisplayLength;
            //int toRow = employeeSearchRequset.PageSize;
            if (employeeSearchRequset.iSortCol_0 == 1)
            {
                employeeSearchRequset.iSortCol_0 = 2;
            }
            Expression<Func<Employee, bool>> query =
                s => ((string.IsNullOrEmpty(employeeSearchRequset.SearchString)) || (s.EmployeeFirstNameE.Contains(employeeSearchRequset.SearchString) || 
                    s.EmployeeMiddleNameE.Contains(employeeSearchRequset.SearchString) || s.EmployeeLastNameE.Contains(employeeSearchRequset.SearchString)) ||
                    (s.EmployeeFirstNameE.Contains(employeeSearchRequset.SearchString) || s.EmployeeMiddleNameE.Contains(employeeSearchRequset.SearchString) ||
                    s.EmployeeLastNameE.Contains(employeeSearchRequset.SearchString)) || (s.JobTitle.JobTitleNameE.Contains(employeeSearchRequset.SearchString)) ||
                    (s.JobTitle.JobTitleNameA.Contains(employeeSearchRequset.SearchString)) || (s.EmployeeJobId == employeeSearchRequset.SearchString) ||
                    (s.JobTitle.Department.DepartmentNameE.Contains(employeeSearchRequset.SearchString)) || (s.JobTitle.Department.DepartmentNameA.Contains(employeeSearchRequset.SearchString)));

            IEnumerable<Employee> employees = employeeSearchRequset.sSortDir_0=="asc" ?
                DbSet
                .Where(query).OrderBy(employeeClause[employeeSearchRequset.EmployeeByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(employeeClause[employeeSearchRequset.EmployeeByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new EmployeeResponse { Employeess = employees, TotalDisplayRecords = DbSet.Count(query), TotalRecords = DbSet.Count(query) };
        }

        /// <summary>
        /// Get all employees in a depertment
        /// </summary>
        public IEnumerable<Employee> GetEmployeesByDepartmentId(long departmentId)
        {
            return DbSet.Where(employee => employee.JobTitle.DepartmentId == departmentId);
        }

        public IEnumerable<Employee> GetRecentEmployees(string requester)
        {
            if (requester == "Admin")
            {
                return DbSet.OrderByDescending(x => x.RecCreatedDt).Take(5);
            }
            long departmentId = Convert.ToInt64(requester);
            return DbSet.Where(x => x.JobTitle.DepartmentId == departmentId).OrderByDescending(x => x.RecCreatedDt).Take(5);
        }

        public Employee FindForPayroll(long employeeId, DateTime currTime)
        {
            return DbSet.FirstOrDefault(employee => employee.EmployeeId == employeeId && employee.Allowances.Count(y=>y.AllowanceDate <= currTime)>0);
        }

        public IEnumerable<string> FindEmployeeEmailById(List<long> employeeId)
        {
            var list = new List<long>();
            if (employeeId !=null)
            {
                list.AddRange(employeeId);
            }
            IEnumerable<string> empIds = DbSet.Where(x => list.Contains(x.EmployeeId)).Select(x => x.Email).ToList();
            return empIds;
        }
    }
}
