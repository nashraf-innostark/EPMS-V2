using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProductRepository : IBaseRepository<Product, long>
    {
        ProductResponse GetByItemVariationId(IEnumerable<long> itemVariationIds, ProductSearchRequest request, long productSectionId);
        IEnumerable<Product> GetByProductSectionId(long productSectionId);
        IEnumerable<Product> SearchInProducts(string search);
        ProductResponse SearchInProducts(ProductSearchRequest request, string search);
        Product FindByVariationId(long variationId);
        ProductListViewResponse GetAllProducts(ProductSearchRequest searchRequest);
    }
}
