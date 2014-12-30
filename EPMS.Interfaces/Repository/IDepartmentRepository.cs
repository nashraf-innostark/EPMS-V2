using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IDepartmentRepository : IBaseRepository<Department, long>
    {
        /// <summary>
        /// Check if Department already exists with the English or Arabic name
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        bool DepartmentExists(Department department);
    }
}
