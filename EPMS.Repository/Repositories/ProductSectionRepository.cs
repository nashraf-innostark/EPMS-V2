using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ProductSectionRepository : BaseRepository<ProductSection>, IProductSectionRepository
    {
        public ProductSectionRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<ProductSection> DbSet
        {
            get { return db.ProductSections; }
        }

        public bool ProductSectionExists(ProductSection productSection)
        {
            if (productSection.SectionId >0)
            {
                return DbSet.Any(
                    section => productSection.SectionId != section.SectionId &&
                        (section.SectionNameEn == productSection.SectionNameEn || section.SectionNameAr == productSection.SectionNameAr));
            }
            return DbSet.Any(
                    section =>
                        (section.SectionNameEn == productSection.SectionNameEn || section.SectionNameAr == productSection.SectionNameAr));
        }
    }
}
