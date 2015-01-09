using System.Collections.Generic;
using EPMS.Models.DomainModels;

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
        /// Add Customer
        /// </summary>
        bool AddCustomer(Customer customer);
        /// <summary>
        /// Update Customer
        /// </summary>
        bool UpdateCustomer(Customer customer);

    }
}
