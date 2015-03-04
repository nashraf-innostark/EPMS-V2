using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class LicenseControlPanelService : ILicenseControlPanelService
    {
        private readonly ILicenseControlPanelRepository Repository;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LicenseControlPanelService(ILicenseControlPanelRepository repository)
        {
            Repository = repository;
        }

        #endregion

        public LicenseControlPanel FindLicenseById(long id)
        {
            return Repository.Find(id);
        }

        public IEnumerable<LicenseControlPanel> GetAll()
        {
            return Repository.GetAll();
        }

        public bool AddLicense(LicenseControlPanel licenseControlPanel)
        {
            try
            {
                Repository.Add(licenseControlPanel);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateLicense(LicenseControlPanel licenseControlPanel)
        {
            try
            {
                Repository.Update(licenseControlPanel);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteLicense(LicenseControlPanel licenseControlPanel)
        {
            Repository.Delete(licenseControlPanel);
            Repository.SaveChanges();
        }
    }
}