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

        public IQueryable<Department> GetEmployeesByDepartment(int id)
        {
            return
                DbSet.Where(x => x.DepartmentId == id);
        }

        public bool DepartmentExists(Department department)
        {
            if (department.DepartmentId > 0) //Alread saved in system
            {
                return DbSet.Any(
                    dept =>
                        department.DepartmentId != dept.DepartmentId &&
                        (dept.DepartmentNameE == department.DepartmentNameE || dept.DepartmentNameA == department.DepartmentNameA));
            }
            // New Department
            return DbSet.Any(
                    dept =>
                        (dept.DepartmentNameE == department.DepartmentNameE || dept.DepartmentNameA == department.DepartmentNameA));
        }
    }
}
