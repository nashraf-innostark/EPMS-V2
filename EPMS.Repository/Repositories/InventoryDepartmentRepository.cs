using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class InventoryDepartmentRepository : BaseRepository<InventoryDepartment>, IInventoryDepartmentRepository
    {
        public InventoryDepartmentRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<InventoryDepartment> DbSet
        {
            get { return db.InventoryDepartments; }
        }

        public bool DepartmentExists(InventoryDepartment department)
        {
            if (department.DepartmentId > 0) //Already in the System
            {
                return DbSet.Any(
                    dept => department.DepartmentId != dept.DepartmentId &&
                        (dept.DepartmentNameEn == department.DepartmentNameEn || dept.DepartmentNameAr == department.DepartmentNameAr));
            }
            return DbSet.Any(
                    dept =>
                        (dept.DepartmentNameEn == department.DepartmentNameEn || dept.DepartmentNameAr == department.DepartmentNameAr));
        }

        public IEnumerable<InventoryDepartment> GetAllDepartments()
        {
            return DbSet.Include(x => x.ParentDepartment).Include(x=>x.InventoryDepartments).ToList();
        }
    }
}
