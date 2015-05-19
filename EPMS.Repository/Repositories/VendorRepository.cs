using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class VendorRepository : BaseRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(IUnityContainer container) 
            : base(container)
        {
        }

        protected override IDbSet<Vendor> DbSet
        {
            get { return db.Vendors; }
        }

        public bool vendorExists(Vendor vendor)
        {
            if (vendor.VendorId > 0) //Already in the System
            {
                return DbSet.Any(
                    ven => vendor.VendorId != ven.VendorId &&
                        (ven.VendorNameEn == vendor.VendorNameEn || ven.VendorNameAr == vendor.VendorNameAr));
            }
            return DbSet.Any(
                    ven => 
                        (ven.VendorNameEn == vendor.VendorNameEn || ven.VendorNameAr == vendor.VendorNameAr));
        }
    }
}
