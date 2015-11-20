using System.Globalization;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class RFQMapper
    {
        public static RFQ CreateFromClientToServer(this WebsiteModels.RFQ source)
        {
            string request = source.Requests==null?"":source.Requests.Replace("\n", "");
            request = request.Replace("\r", "");
            return new RFQ
            {
                RFQId = source.RFQId,
                SerialNumber = source.SerialNumber,
                CustomerId = source.CustomerId,
                Notes = source.Notes,
                Discount = source.Discount,
                Status = source.Status,
                Requests = request,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                RFQItems = source.RFQItems.Select(x=>x.CreateFromClientToServer()).ToList()
            };
        }
        public static WebsiteModels.RFQ CreateFromServerToClient(this RFQ source)
        {
            return new WebsiteModels.RFQ
            {
                RFQId = source.RFQId,
                SerialNumber = source.SerialNumber,
                CustomerId = source.CustomerId,
                Notes = source.Notes,
                Discount = source.Discount,
                Status = source.Status,
                Requests = source.Requests,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                RFQItems = source.RFQItems.Select(x=>x.CreateFromServerToClient()).ToList(),
                CustomerNameEn = source.Customer != null ? source.Customer.CustomerNameE : "",
                CustomerNameAr = source.Customer != null ? source.Customer.CustomerNameA : "",
            };
        }

        public static WebsiteModels.RfqDropDown CreateForDropDown(this RFQ source)
        {
            return new WebsiteModels.RfqDropDown
            {
                RFQId = source.RFQId,
                SerialNumber = source.SerialNumber
            };
        }
    }
}
