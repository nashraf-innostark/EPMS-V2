using System.Linq;
using EPMS.Models.RequestModels;
using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;


namespace EPMS.Web.ModelMappers
{
    public static class VendorMapper
    {
        public static WebModels.Vendor CreateFromServerToClient(this DomainModels.Vendor source)
        {
            return new WebModels.Vendor
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

        public static VendorRequest CreateFromClientToServer(this WebModels.Vendor source)
        {
            var vendor = new DomainModels.Vendor();

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

        public static DomainModels.VendorItem CreateFrom(this WebModels.VendorItems source)
        {
            return new DomainModels.VendorItem
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                VendorId = source.VendorId
            };
        }

    }
}