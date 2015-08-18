using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProductRepository : IBaseRepository<Product, long>
    {
        IList<Product> GetByItemVariationId(IEnumerable<long> itemVariationIds, ProductSearchRequest request);
        IEnumerable<Product> GetByProductSectionId(long productSectionId);
    }
}
