using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface ILicenseControlPanelService
    {
        LicenseControlPanel FindLicenseById(long id);
        IEnumerable<LicenseControlPanel> GetAll();
        bool AddLicense(LicenseControlPanel licenseControlPanel);
        bool UpdateLicense(LicenseControlPanel licenseControlPanel);
        void DeleteLicense(LicenseControlPanel licenseControlPanel);
    }
}