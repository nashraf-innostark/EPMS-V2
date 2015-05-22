using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;


namespace EPMS.Repository.Repositories
{
    class SizeRepository : BaseRepository<Size>, ISizeRepository
    {
        public SizeRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Size> DbSet
        {
            get { return db.Sizes; }
        }

        public bool SizeExists(Size size)
        {
            if (size.SizeId > 0) //Already in the System
            {
                return DbSet.Any(
                    sz => size.SizeId != sz.SizeId &&
                        (sz.SizeNameEn == size.SizeNameEn || sz.SizeNameAr == size.SizeNameAr));
            }
            return DbSet.Any(
                    sz =>
                        (sz.SizeNameEn == size.SizeNameEn || sz.SizeNameAr == size.SizeNameAr));
        }
    }
}
