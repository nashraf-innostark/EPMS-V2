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
        public DepartmentResponse GetAllDepartment(DepartmentSearchRequest departmentSearchRequest)
        {
            return departmentRepository.GetAllDepartment(departmentSearchRequest);
        }

        public Department FindDepartmentById(long? id)
        {
            if (id != null) return departmentRepository.Find((long)id);
            return null;
        }

        public bool AddDepartment(Department department)
        {
            try
            {
                departmentRepository.Add(department);
                departmentRepository.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        public bool UpdateDepartment(Department department)
        {
            try
            {
                departmentRepository.Update(department);
                departmentRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void DeleteDepartment(Department department)
        {
            departmentRepository.Delete(department);
            departmentRepository.SaveChanges();
        }

        /// <summary>
        /// Finds Employees by Department ID
        /// </summary>
        public IEnumerable<Employee> FindEmployeeByDeprtmentId(int depertmentId)
        {
            return employeeRepository.GetEmployeesByDepartmentId(depertmentId);
        }
    }
}
