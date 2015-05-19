using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IVendorRepository : IBaseRepository<Vendor, long>
    {
        bool vendorExists (Vendor vendor);
    }
}
