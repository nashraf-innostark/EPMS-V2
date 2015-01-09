using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        #region Constructor

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        #endregion

        public IEnumerable<Customer> GetAll()
        {
            return customerRepository.GetAll();
        }

        public Customer FindCustomerById(long id)
        {
            return customerRepository.Find(id);
        }

        public bool AddCustomer(Customer customer)
        {
            customerRepository.Add(customer);
            customerRepository.SaveChanges();
            return true;
        }

        public bool UpdateCustomer(Customer customer)
        {
            customerRepository.Update(customer);
            customerRepository.SaveChanges();
            return true;
        }
    }
}
