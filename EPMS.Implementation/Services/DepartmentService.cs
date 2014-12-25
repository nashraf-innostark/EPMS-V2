using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDepartmentRepository departmentRepository;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DepartmentService(IDepartmentRepository xDepartmentRepository, IEmployeeRepository employeeRepository)
        {
            if (xDepartmentRepository == null) throw new ArgumentNullException("xDepartmentRepository");
            if (employeeRepository == null) throw new ArgumentNullException("employeeRepository");
            this.employeeRepository = employeeRepository;
            departmentRepository = xDepartmentRepository;
        }

        #endregion


        public IEnumerable<Department> GetAll()
        {
            return departmentRepository.GetAll();
        }

        public Department FindDepartmentById(long id)
        {
            return departmentRepository.Find(id);
        }

        public bool AddDepartment(Department department)
        {
            if (departmentRepository.DepartmentExists(department))
            {
                throw new InvalidOperationException("Depatment with the same name already exists.");
            }
            departmentRepository.Add(department);
            departmentRepository.SaveChanges();
            return true;
        }
        public bool UpdateDepartment(Department department)
        {
            if (departmentRepository.DepartmentExists(department))
            {
                throw new InvalidOperationException("Depatment with the same name already exists.");
            }
            departmentRepository.Update(department);
            departmentRepository.SaveChanges();
            return true;
        }
        public void DeleteDepartment(Department department)
        {
            departmentRepository.Delete(department);
            departmentRepository.SaveChanges();
        }

        /// <summary>
        /// Finds Employees by Department ID
        /// </summary>
        public IEnumerable<Employee> FindEmployeeByDeprtmentId(long? depertmentId)
        {
            return employeeRepository.GetEmployeesByDepartmentId((long)depertmentId);
        }
    }
}
