using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IWebsiteCustomerService
    {
        WebsiteCustomer FindWebsiteCustomerById(long customerId);
        IEnumerable<WebsiteCustomer> GetAll(); 
        bool AddWebsiteCustomer(WebsiteCustomer customer);
        bool UpdateWebsiteCustomer(WebsiteCustomer customer);
        void DeleteWebsiteCustomer(WebsiteCustomer customer);
    }
}
