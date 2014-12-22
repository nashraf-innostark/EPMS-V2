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
using Microsoft.Practices.Unity;
using EPMS.Repository.BaseRepository;

namespace EPMS.Repository.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DepartmentRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Department> DbSet
        {
            get { return db.Departments; }
        }

        #endregion

        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<DepartmentByColumn, Func<Department, object>> departmentClause =

            new Dictionary<DepartmentByColumn, Func<Department, object>>
                    {
                        { DepartmentByColumn.DepartmentId, c => c.DepartmentId},
                        { DepartmentByColumn.DepartmentName,  c => c.DepartmentName},
                        { DepartmentByColumn.DepartmentDesc, c => c.DepartmentDesc}
                    };
        #endregion

        /// <summary>
        /// Get all Department with filters if any
        /// </summary>
        /// <param name="departmentSearchRequest"></param>
        /// <returns>Department Repsonse</returns>
        public DepartmentResponse GetAllDepartment(DepartmentSearchRequest departmentSearchRequest)
        {
            int fromRow = (departmentSearchRequest.PageNo - 1) * departmentSearchRequest.PageSize;
            int toRow = departmentSearchRequest.PageSize;

            Expression<Func<Department, bool>> query =
                s => (((departmentSearchRequest.DepartmentId == 0) || s.DepartmentId == departmentSearchRequest.DepartmentId
                    || s.DepartmentId == departmentSearchRequest.DepartmentId) &&
                    (string.IsNullOrEmpty(departmentSearchRequest.DepartmentName)
                    || (s.DepartmentName.Contains(departmentSearchRequest.DepartmentName))));

            IEnumerable<Department> departments = departmentSearchRequest.IsAsc ?
                DbSet
                .Where(query).OrderBy(departmentClause[departmentSearchRequest.DepapartmentByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(departmentClause[departmentSearchRequest.DepapartmentByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new DepartmentResponse { Departments  = departments, TotalCount = DbSet.Count(query) };
        }

        public IQueryable<Department> GetEmployeesByDepartment(int id)
        {
            return
                DbSet.Where(x => x.DepartmentId == id);
        }
    }
}
