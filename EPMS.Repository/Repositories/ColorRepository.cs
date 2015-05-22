using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Repository.BaseRepository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class ColorRepository : BaseRepository<Color>, IColorRepository
    {
        public ColorRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Color> DbSet
        {
            get { return db.Colors; }
        }

        public bool ColorExists(Color color)
        {
            if (color.ColorId > 0) //Already in the System
            {
                return DbSet.Any(
                    col => color.ColorId != col.ColorId &&
                        (col.ColorNameEn == color.ColorNameEn || col.ColorNameAr == color.ColorNameAr));
            }
            return DbSet.Any(
                    col =>
                        (col.ColorNameEn == color.ColorNameEn || col.ColorNameAr == color.ColorNameAr));
        }
    }
}
