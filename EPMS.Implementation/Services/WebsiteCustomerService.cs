using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class WebsiteCustomerService : IWebsiteCustomerService
    {
        #region Private
        private readonly IWebsiteCustomerRepository repository;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public WebsiteCustomerService(IWebsiteCustomerRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        public WebsiteCustomer FindWebsiteCustomerById(long customerId)
        {
            return repository.Find(customerId);
        }

        public IEnumerable<WebsiteCustomer> GetAll()
        {
            return repository.GetAll();
        }

        public bool AddWebsiteCustomer(WebsiteCustomer customer)
        {
            try
            {
                repository.Add(customer);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateWebsiteCustomer(WebsiteCustomer customer)
        {
            try
            {
                repository.Update(customer);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteWebsiteCustomer(WebsiteCustomer customer)
        {
            repository.Delete(customer);
            repository.SaveChanges();
        }
    }
}
