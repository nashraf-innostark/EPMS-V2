using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<ProductImage> DbSet
        {
            get { return db.ProductImages; }
        }
    }
}
