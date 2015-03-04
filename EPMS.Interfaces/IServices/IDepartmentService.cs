using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    /// <summary>
    /// Department 
    /// </summary>
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAll();
        //DepartmentResponse GetAllDepartment(DepartmentSearchRequest departmentSearchRequest);

        Department FindDepartmentById(long id);

        bool AddDepartment(Department department);
        bool UpdateDepartment(Department department);
        void DeleteDepartment(Department department);

        IEnumerable<Employee> FindEmployeeByDeprtmentId(long? depertmentId);
    }
}
