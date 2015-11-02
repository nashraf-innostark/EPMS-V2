using System.Linq;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class VendorMapper
    {
        public static WebsiteModels.Vendor CreateFromServerToClient(this Models.DomainModels.Vendor source)
        {
            return new WebsiteModels.Vendor
            {
                VendorId = source.VendorId,
                VendorNameEn = source.VendorNameEn,
                VendorNameAr = source.VendorNameAr,
                ContactPerson = source.ContactPerson,
                VendorEmail = source.VendorEmail,
                VendorContact = source.VendorContact,
                VendorField = source.VendorField,
                DetailsEn = source.DetailsEn,
                DetailsAr = source.DetailsAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                PurchaseOrderItems = source.PurchaseOrderItems.Select(x=>x.CreateFromServerToClient()).ToList()
            };
        }

        public static VendorRequest CreateFromClientToServer(this WebsiteModels.Vendor source)
        {
            var vendor = new Models.DomainModels.Vendor();

            vendor.VendorId = source.VendorId;
            vendor.VendorNameEn = source.VendorNameEn;
            vendor.VendorNameAr = source.VendorNameAr;
            vendor.ContactPerson = source.ContactPerson;
            vendor.VendorEmail = source.VendorEmail;
            vendor.VendorContact = source.VendorContact;
            vendor.VendorField = source.VendorField;
            vendor.DetailsEn = source.DetailsEn;
            vendor.DetailsAr = source.DetailsAr;
            vendor.RecCreatedBy = source.RecCreatedBy;
            vendor.RecCreatedDt = source.RecCreatedDt;
            vendor.RecLastUpdatedBy = source.RecLastUpdatedBy;
            vendor.RecLastUpdatedDt = source.RecLastUpdatedDt;
            var request = new VendorRequest();
            request.Vendor = vendor;
            request.VendorItems = source.VendorItems;
            return request;
        }

        public static Models.DomainModels.VendorItem CreateFrom(this WebsiteModels.VendorItems source)
        {
            return new Models.DomainModels.VendorItem
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                VendorId = source.VendorId
            };
        }

    }
}