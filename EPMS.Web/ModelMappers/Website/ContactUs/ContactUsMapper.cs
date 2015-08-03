using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.Website.ContactUs
{
    public static class ContactUsMapper
    {
        public static WebModels.ContactUs CreateFromServerToClient(this DomainModels.ContactUs source)
        {
            return new WebModels.ContactUs
            {
                ContactUsId = source.ContactUsId,
                Address = source.Address,
                AddressAr = source.AddressAr,
                Website = source.Website,
                Title = source.Title,
                TitleAr = source.TitleAr,
                ContentAr = source.ContentAr,
                ContentEn = source.ContentEn,
                FormEmail = source.FormEmail,
                ShowToPublic = source.ShowToPublic,
                Latitude = source.Latitude,
                Longitude = source.Longitude,
                Mobile = source.Mobile,
                POBox = source.POBox,
                Phone = source.Phone,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }

        public static DomainModels.ContactUs CreateFromClientToServer(this WebModels.ContactUs source)
        {
            return new DomainModels.ContactUs
            {
                ContactUsId = source.ContactUsId,
                Address = source.Address,
                AddressAr = source.AddressAr,
                Website = source.Website,
                Title = source.Title,
                TitleAr = source.TitleAr,
                ContentAr = source.ContentAr,
                ContentEn = source.ContentEn,
                FormEmail = source.FormEmail,
                ShowToPublic = source.ShowToPublic,
                Latitude = source.Latitude,
                Longitude = source.Longitude,
                Mobile = source.Mobile,
                POBox = source.POBox,
                Phone = source.Phone,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}