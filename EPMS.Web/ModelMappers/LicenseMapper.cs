using System;
using System.Globalization;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class LicenseMapper
    {
        public static LicenseControlPanel CreateFromClientToServer(this Models.LicenseControlPanel source)
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

        public static Models.LicenseControlPanel CreateFromServerToClient(this LicenseControlPanel source)
        {
            return new Models.LicenseControlPanel
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