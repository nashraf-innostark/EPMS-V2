using System.Linq;
using EPMS.Web.ViewModels.Customer;
using ApiModels = EPMS.Web.Models;
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
                CustomerName = source.CustomerName,
                CustomerAddress = source.CustomerAddress,
                CustomerMobile = source.CustomerMobile,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                OrdersCount = source.Orders.Count,
                //LatestComplaint = source.Complaints.OrderByDescending(c=>c.ComplaintDate).Where(c=>c.CustomerId == source.CustomerId).FirstOrDefault().Topic ?? "",
                //LatestOrder = source.Orders.OrderByDescending(c => c.OrderDate).Where(c => c.CustomerId == source.CustomerId).FirstOrDefault().OrderNo ?? "",
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
                CustomerName = source.CustomerName,
                CustomerAddress = source.CustomerAddress,
                CustomerMobile = source.CustomerMobile,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };

        }
    }
}