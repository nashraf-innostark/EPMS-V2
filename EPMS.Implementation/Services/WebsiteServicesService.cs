﻿using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

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
            return servicesRepository.Find(id);
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
    }
}
