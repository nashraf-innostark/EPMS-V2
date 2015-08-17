using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product FindProductById(long id);
        bool AddProduct(Product product);
        bool UpdateProduct(Product product);
        void DeleteProduct(Product product);
        bool Delete(long id);
        ProductResponse ProductResponse(long id);
        ProductResponse SaveProduct(ProductRequest productToSave);
        bool SaveProducts(IList<Product> products);
        ProductsListResponse GetProductsList(long id, string from);
        ProductDetailResponse GetProductDetails(long id, string from);
    }
}
