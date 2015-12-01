using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    /// <summary>
    /// Customer
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Get All Customers
        /// </summary>
        IEnumerable<Customer> GetAll();
        /// <summary>
        /// Find Customer By Id
        /// </summary>
        Customer FindCustomerById(long id);
        /// <summary>
        /// Get ContactList Response
        /// </summary>
        /// <returns></returns>
        ContactListResponse GetContactListResponse();
        /// <summary>
        /// Add Customer
        /// </summary>
        Customer AddCustomer(Customer customer);
        /// <summary>
        /// Update Customer
        /// </summary>
        bool UpdateCustomer(Customer customer);
        /// <summary>
        /// Get Customer and Employees' list
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        CustomerResponse GetCustomerResponse(long customerId);

    }
}
