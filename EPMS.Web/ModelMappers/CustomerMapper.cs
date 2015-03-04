using System.Linq;
using EPMS.Web.ViewModels.Customer;
using Models = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class CustomerMapper
    {
        public static Models.Customer CreateFromServerToClient(this DomainModels.Customer source)
        {
            return new Models.Customer
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
        public static DashboardModels.Customer CreateForDashboard(this DomainModels.Customer source)
        {
            return new DashboardModels.Customer
            {
                CustomerId = source.CustomerId,
                CustomerNameE = source.CustomerNameE,
                CustomerNameA = source.CustomerNameA
            };

        }
        public static CustomerViewModel CreateFromServerToClientVM(this DomainModels.Customer source)
        {
            return new CustomerViewModel
            {
                Customer = source.CreateFromServerToClient(),
                User = source.AspNetUsers.FirstOrDefault(x => x.CustomerId == source.CustomerId)
            };

        }
        public static DomainModels.Customer CreateFrom(this Models.Customer source)
        {
            return new DomainModels.Customer
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

        public static Models.ContactList CreateForContactList(this DomainModels.Customer source)
        {
            Models.ContactList contactList = new Models.ContactList();
            contactList.NameE = source.CustomerNameE ?? "";
            contactList.NameA = source.CustomerNameA ?? "";
            contactList.Link = "/CMS/Customer/Create/" + source.CustomerId;
            contactList.Type = "Customer";
            contactList.MobileNumber = source.CustomerMobile ?? "";
            var firstOrDefault = source.AspNetUsers.Where(x=>x.CustomerId==source.CustomerId).FirstOrDefault();
            if (firstOrDefault != null)
                contactList.Email = firstOrDefault.Email;
            return contactList;
        }
    }
}