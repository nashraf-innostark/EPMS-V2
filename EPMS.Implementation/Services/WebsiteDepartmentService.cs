using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class WebsiteDepartmentService : IWebsiteDepartmentService
    {
        #region Private

        private readonly IWebsiteDepartmentRepository repository;
        
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WebsiteDepartmentService(IWebsiteDepartmentRepository repository)
        {
            this.repository = repository;
        }

        #endregion
        public IEnumerable<WebsiteDepartment> GetAll()
        {
            return repository.GetAll();
        }

        public WebsiteDepartment FindDepartmentById(long id)
        {
            return repository.Find(id);
        }

        public bool AddDepartment(WebsiteDepartment websiteDepartment)
        {
            try
            {
                repository.Add(websiteDepartment);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateDepartment(WebsiteDepartment websiteDepartment)
        {
            try
            {
                repository.Update(websiteDepartment);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteDepartment(long id)
        {
            var dataToDelete = repository.Find(id);
            if (dataToDelete != null)
            {
                repository.Delete(dataToDelete);
                repository.SaveChanges();
            }
        }
    }
}
