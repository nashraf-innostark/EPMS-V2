using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class WebsiteServicesService : IWebsiteServicesService
    {
        #region Private

        private readonly IWebsiteServicesRepository servicesRepository;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WebsiteServicesService(IWebsiteServicesRepository servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        #endregion

        public WebsiteService FindWebsiteServiceById(long id)
        {
            var result = servicesRepository.Find(id);
            return result;
        }

        public IEnumerable<WebsiteService> GetAll()
        {
            return servicesRepository.GetAll();
        }

        public bool AddWebsiteService(WebsiteService service)
        {
            try
            {
                servicesRepository.Add(service);
                servicesRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateWebsiteService(WebsiteService service)
        {
            try
            {
                servicesRepository.Update(service);
                servicesRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteWebsiteService(long serviceId)
        {
            WebsiteService dataToDelete = servicesRepository.Find(serviceId);
            if (dataToDelete != null)
            {
                servicesRepository.Delete(dataToDelete);
                servicesRepository.SaveChanges();
            }
        }

        public WebsiteServicesCreateResponse LoadWebsiteServices(long id)
        {
            WebsiteServicesCreateResponse response = new WebsiteServicesCreateResponse
            {
                WebsiteService = id > 0 ? servicesRepository.Find(id) : null,
                WebsiteServices = servicesRepository.GetAll()
            };
            return response;
        }
    }
}