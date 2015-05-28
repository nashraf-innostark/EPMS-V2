using System;
using System.Collections.Generic;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Implementation.Services
{
    class InventoryDepartmentService : IInventoryDepartmentService
    {
        private readonly IInventoryDepartmentRepository departmentRepository;

        public InventoryDepartmentService(IInventoryDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public IEnumerable<InventoryDepartment> GetAll()
        {
            return departmentRepository.GetAllDepartments();
        }

        public InventoryDepartment FindInventoryDepartmentById(long id)
        {
            return departmentRepository.Find(id);
        }

        public bool AddDepartment(InventoryDepartment department)
        {
            if (departmentRepository.DepartmentExists(department))
            {
                throw new ArgumentException("Department Already Exists");
            }
            departmentRepository.Add(department);
            departmentRepository.SaveChanges();
            return true;
        }

        public bool UpdateDepartment(InventoryDepartment department)
        {
            if (departmentRepository.DepartmentExists(department))
            {
                throw new ArgumentException("Department Already Exists");
            }
            departmentRepository.Update(department);
            departmentRepository.SaveChanges();
            return true;
        }

        public void DeleteDepartment(InventoryDepartment department)
        {
            departmentRepository.Delete(department);
            departmentRepository.SaveChanges();
        }

        public SaveInventoryDepartmentResponse SaveDepartment(InventoryDepartmentRequest departmentToSave)
        {
            #region Add

            if (departmentToSave.InventoryDepartment.DepartmentId > 0)
            {
                UpdateInventoryDepartment(departmentToSave.InventoryDepartment);
            }

            #endregion

            #region Update

            else
            {
                SaveInventoryDepartment(departmentToSave.InventoryDepartment);
            }

            #endregion

            return new SaveInventoryDepartmentResponse();
        }

        private void SaveInventoryDepartment(InventoryDepartment department)
        {
            department.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            department.RecCreatedDt = DateTime.Now;
            department.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            department.RecLastUpdatedDt = DateTime.Now;
            departmentRepository.Add(department);
            departmentRepository.SaveChanges();
        }
        private void UpdateInventoryDepartment(InventoryDepartment department)
        {
            department.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            department.RecLastUpdatedDt = DateTime.Now;
            departmentRepository.Update(department);
            departmentRepository.SaveChanges();
        }
    }
}
