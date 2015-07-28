using System.Collections.Generic;
using EPMS.Models.DomainModels;

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
    }
}
