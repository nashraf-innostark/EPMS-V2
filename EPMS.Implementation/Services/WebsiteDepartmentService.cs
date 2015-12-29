using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class WebsiteDepartmentService : IWebsiteDepartmentService
    {
        #region Private

        private readonly IWebsiteDepartmentRepository repository;
        private readonly IProductSectionService productSectionService;

        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WebsiteDepartmentService(IWebsiteDepartmentRepository repository, IProductSectionService productSectionService)
        {
            this.repository = repository;
            this.productSectionService = productSectionService;
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
                var oldData = repository.Find(websiteDepartment.DepartmentId);
                if (oldData != null)
                {
                    websiteDepartment.RecCreatedBy = oldData.RecCreatedBy;
                    websiteDepartment.RecCreatedDate = oldData.RecCreatedDate;
                    repository.Update(websiteDepartment);
                    repository.SaveChanges();
                    return true;
                }
                return false;
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

        public WebsiteDepartmentResponse websiteDepartmentResponse(long id)
        {
            WebsiteDepartmentResponse response = new WebsiteDepartmentResponse();
            response.ProductSections = productSectionService.GetAll();
            response.websiteDepartment = repository.Find(id);
            return response;
        }
    }
}
