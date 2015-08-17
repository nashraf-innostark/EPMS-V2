using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProductRepository : IBaseRepository<Product, long>
    {
        Product GetByItemVariationId(long itemVariationId);
        IEnumerable<Product> GetByProductSectionId(long productSectionId);
    }
}
