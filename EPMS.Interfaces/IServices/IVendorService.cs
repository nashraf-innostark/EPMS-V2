using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IVendorService
    {
        /// <summary>
        /// Get All Vendors
        /// </summary>
        IEnumerable<Vendor> GetAll();
        /// <summary>
        /// Find Vendor By Id
        /// </summary>
        Vendor FindVendorById (long id);
        /// <summary>
        /// Add Vendor
        /// </summary>
        bool AddVendor(Vendor vendor);
        /// <summary>
        /// Update Vendor
        /// </summary>
        bool UpdateVendor(Vendor vendor);
        /// <summary>
        /// Delete Vendor
        /// </summary>
        void DeleteVendor(Vendor vendor);
        SaveVendorResponse SaveVendor(VendorRequest vendorToSave);
    }
}
