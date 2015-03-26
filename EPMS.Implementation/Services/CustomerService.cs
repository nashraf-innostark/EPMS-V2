using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IEmployeeService employeeService;
        private readonly IJobApplicantService jobApplicantService;

        #region Constructor

        public CustomerService(ICustomerRepository customerRepository, IEmployeeService employeeService, IJobApplicantService jobApplicantService)
        {
            this.employeeService = employeeService;
            this.jobApplicantService = jobApplicantService;
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

        public ContactListResponse GetContactListResponse()
        {
            ContactListResponse response = new ContactListResponse
            {
                Customers = customerRepository.GetAll(),
                Employees = employeeService.GetAll(),
                JobApplicants = jobApplicantService.GetAll()
            };
            return response;
        }
        public Customer AddCustomer(Customer customer)
        {
            customerRepository.Add(customer);
            customerRepository.SaveChanges();
            return customer;
        }

        public bool UpdateCustomer(Customer customer)
        {
            customerRepository.Update(customer);
            customerRepository.SaveChanges();
            return true;
        }
    }
}
