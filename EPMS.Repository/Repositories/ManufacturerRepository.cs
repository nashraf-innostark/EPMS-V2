using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class ManufacturerRepository : BaseRepository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Manufacturer> DbSet
        {
            get { return db.Manufacturers; }
        }

        public bool ManufacturerExists(Manufacturer manufacturer)
        {
            if (manufacturer.ManufacturerId > 0) //Already in the System
            {
                return DbSet.Any(
                    manu => manufacturer.ManufacturerId != manu.ManufacturerId &&
                        (manu.ManufacturerNameEn == manufacturer.ManufacturerNameEn || manu.ManufacturerNameAr == manufacturer.ManufacturerNameAr));
            }
            return DbSet.Any(
                    manu =>
                        (manu.ManufacturerNameEn == manufacturer.ManufacturerNameEn || manu.ManufacturerNameAr == manufacturer.ManufacturerNameAr));
        }
    }
}
