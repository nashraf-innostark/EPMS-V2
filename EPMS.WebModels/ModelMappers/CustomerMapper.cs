using System.Linq;
using EPMS.WebModels.ViewModels.Customer;

namespace EPMS.WebModels.ModelMappers
{
    public static class CustomerMapper
    {
        public static WebsiteModels.Customer CreateFromServerToClient(this Models.DomainModels.Customer source)
        {
            return new WebsiteModels.Customer
            {

                CustomerId = source.CustomerId,
                CustomerNameE = source.CustomerNameE,
                CustomerNameA = source.CustomerNameA,
                CustomerAddress = source.CustomerAddress,
                CustomerMobile = source.CustomerMobile,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                OrdersCount = source.Orders.Count,
                LatestComplaint =
                    (source.Complaints != null && source.Complaints.Any(c => c.CustomerId == source.CustomerId))
                        ? source.Complaints.Where(c => c.CustomerId == source.CustomerId)
                            .OrderByDescending(c => c.ComplaintDate)
                            .FirstOrDefault()
                            .Topic
                        : string.Empty,
                LatestOrder =
                    (source.Orders != null && source.Orders.Any(c => c.CustomerId == source.CustomerId))
                        ? source.Orders.Where(c => c.CustomerId == source.CustomerId)
                            .OrderBy(c => c.OrderDate)
                            .FirstOrDefault()
                            .OrderNo
                        : string.Empty,
            };

        }
        /// <summary>
        /// Map customers from database to client dashboard
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Models.DashboardModels.Customer CreateForDashboard(this Models.DomainModels.Customer source)
        {
            return new Models.DashboardModels.Customer
            {
                CustomerId = source.CustomerId,
                CustomerNameE = source.CustomerNameE,
                CustomerNameA = source.CustomerNameA
            };

        }
        public static CustomerViewModel CreateFromServerToClientVM(this Models.DomainModels.Customer source)
        {
            return new CustomerViewModel
            {
                Customer = source.CreateFromServerToClient(),
                User = source.AspNetUsers.FirstOrDefault(x => x.CustomerId == source.CustomerId)
            };

        }
        public static Models.DomainModels.Customer CreateFrom(this WebsiteModels.Customer source)
        {
            return new Models.DomainModels.Customer
            {
                CustomerId = source.CustomerId,
                CustomerNameE = source.CustomerNameE,
                CustomerNameA = source.CustomerNameA,
                CustomerAddress = source.CustomerAddress,
                CustomerMobile = source.CustomerMobile,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };

        }

        public static WebsiteModels.ContactList CreateForContactList(this Models.DomainModels.Customer source)
        {
            WebsiteModels.ContactList contactList = new WebsiteModels.ContactList();
            contactList.NameE = source.CustomerNameE ?? "";
            contactList.NameA = source.CustomerNameA ?? "";
            contactList.Link = "CMS/Customer/Details/" + source.CustomerId;
            contactList.Type = "Customer";
            contactList.MobileNumber = source.CustomerMobile ?? "";
            var firstOrDefault = source.AspNetUsers.Where(x=>x.CustomerId==source.CustomerId).FirstOrDefault();
            if (firstOrDefault != null)
                contactList.Email = firstOrDefault.Email;
            return contactList;
        }
    }
}