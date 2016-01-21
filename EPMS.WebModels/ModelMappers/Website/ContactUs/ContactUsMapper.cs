using System;
using System.Globalization;

namespace EPMS.WebModels.ModelMappers.Website.ContactUs
{
    public static class ContactUsMapper
    {
        public static WebsiteModels.ContactUs CreateFromServerToClient(this Models.DomainModels.ContactUs source)
        {
            return new WebsiteModels.ContactUs
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
                RecCreatedDt = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }

        public static Models.DomainModels.ContactUs CreateFromClientToServer(this WebsiteModels.ContactUs source)
        {
            return new Models.DomainModels.ContactUs
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
                RecCreatedDt = DateTime.ParseExact(source.RecCreatedDt, "dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}