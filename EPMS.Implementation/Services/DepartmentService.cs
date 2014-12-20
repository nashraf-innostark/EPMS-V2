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
        private readonly IDepartmentRepository repository;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public DepartmentService(IDepartmentRepository xRepository)
        {
            repository = xRepository;
        }

        #endregion


        public IEnumerable<Department> GetAll()
        {
            return repository.GetAll();
        }
        public DepartmentResponse GetAllDepartment(DepartmentSearchRequest departmentSearchRequest)
        {
            return repository.GetAllDepartment(departmentSearchRequest);
        }

        public Department FindDepartmentById(long id)
        {
            return repository.Find(id);
        }

        public bool AddDepartment(Department department)
        {
            try
            {
                repository.Add(department);
                repository.SaveChanges();
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
                repository.Update(department);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void DeleteDepartment(Department department)
        {
            try
            {
                repository.Delete(department);
                repository.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
