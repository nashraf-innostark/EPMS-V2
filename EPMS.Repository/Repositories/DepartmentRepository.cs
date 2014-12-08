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
                        { DepartmentByColumn.DepartmentNameE,  c => c.DepartmentNameE},
                        { DepartmentByColumn.DepartmentNameA, c => c.DepartmentNameA},
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
                    (string.IsNullOrEmpty(departmentSearchRequest.DepartmentNameE)
                    || (s.DepartmentNameE.Contains(departmentSearchRequest.DepartmentNameE))));

            IEnumerable<Department> departments = departmentSearchRequest.IsAsc ?
                DbSet
                .Where(query).OrderBy(departmentClause[departmentSearchRequest.DepapartmentByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(departmentClause[departmentSearchRequest.DepapartmentByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new DepartmentResponse { Departments  = departments, TotalCount = DbSet.Count(query) };
        }

        /// <summary>
        /// Get department by Department ID
        /// </summary>
        /// <param name="id">Department ID</param>
        /// <returns></returns>
        public Department FindDepartmentById(int? id)
        {
            return DbSet.FirstOrDefault(departmentId => departmentId.DepartmentId == id);
        }

        /// <summary>
        /// Load all Departmrnts
        /// </summary>
        /// <returns>List of all Deparments</returns>
        public IEnumerable<Department> LoadAll()
        {
            return DbSet.ToList();
        }
    }
}
