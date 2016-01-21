using System.Linq;
using EPMS.WebModels.ViewModels.Customer;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class CustomerMapper
    {
        public static Customer CreateFromServerToClient(this Models.DomainModels.Customer source)
        {
            return new Customer
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
                EmployeeId = source.EmployeeId,
                OrdersCount = source.Orders.Count,
                ComplaintsCount = source.Complaints.Count,
                Email = source.AspNetUsers != null && source.AspNetUsers.Any() ? source.AspNetUsers.FirstOrDefault(x => x.CustomerId == source.CustomerId).Email : "",
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
                            .OrderBy(c => c.RecCreatedDate)
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
        public static Models.DomainModels.Customer CreateFrom(this Customer source)
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
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                EmployeeId = source.EmployeeId
            };

        }

        public static ContactList CreateForContactList(this Models.DomainModels.Customer source)
        {
            ContactList contactList = new ContactList();
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

        public static CustomerDropDown CreateForDropDown(this Models.DomainModels.Customer source)
        {
            return new CustomerDropDown
            {
                CustomerId = source.CustomerId,
                CustomerNameE = source.CustomerNameE,
                CustomerNameA = source.CustomerNameA
            };
        }

        public static Customer CreateForRfq(this Models.DomainModels.Customer source)
        {
            return new Customer
            {
                CustomerId = source.CustomerId,
                CustomerNameE = source.CustomerNameE,
                CustomerNameA = source.CustomerNameA,
                CustomerAddress = source.CustomerAddress,
                CustomerMobile = source.CustomerMobile,
                EmployeeId = source.EmployeeId,
                Email = source.AspNetUsers != null ? source.AspNetUsers.FirstOrDefault(x => x.CustomerId == source.CustomerId).Email : "",
            };
        }
    }
}