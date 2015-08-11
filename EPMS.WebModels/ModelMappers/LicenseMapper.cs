using System;
using System.Globalization;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class LicenseMapper
    {
        public static LicenseControlPanel CreateFromClientToServer(this WebsiteModels.LicenseControlPanel source)
        {
            return new LicenseControlPanel
            {
                LicenseControlPanelId = source.LicenseControlPanelId,
                CompanyName = source.CompanyName,
                Website = source.Website,
                Email = source.Email,
                Address = source.Address,
                LandLine = source.LandLine,
                Mobile = source.Mobile,
                CommercialRegister = source.CommercialRegister,
                ProductNumber = source.ProductNumber,
                NoOfUsers = source.NoOfUsers,
                LicenseNumber = source.LicenseNumber,
                StartDate = DateTime.ParseExact(source.StartDate, "dd/MM/yyyy", new CultureInfo("en")),
                EndDate = DateTime.ParseExact(source.EndDate, "dd/MM/yyyy", new CultureInfo("en")),
                Status = source.Status
            };
        }

        public static WebsiteModels.LicenseControlPanel CreateFromServerToClient(this LicenseControlPanel source)
        {
            return new WebsiteModels.LicenseControlPanel
            {
                LicenseControlPanelId = source.LicenseControlPanelId,
                CompanyName = source.CompanyName,
                Website = source.Website,
                Email = source.Email,
                Address = source.Address,
                LandLine = source.LandLine,
                Mobile = source.Mobile,
                CommercialRegister = source.CommercialRegister,
                ProductNumber = source.ProductNumber,
                NoOfUsers = source.NoOfUsers,
                LicenseNumber = source.LicenseNumber,
                StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en")),
                Status = source.Status
            };
        }
    }
}