using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    /// <summary>
    /// Department 
    /// </summary>
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAll();
        DepartmentResponse GetAllDepartment(DepartmentSearchRequest departmentSearchRequest);

        Department FindDepartmentById(long? id);

        bool AddDepartment(Department department);
        bool UpdateDepartment(Department department);
        void DeleteDepartment(Department department);

        IEnumerable<Employee> FindEmployeeByDeprtmentId(int depertmentId);
    }
}
