using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProductImageService
    {
        IEnumerable<ProductImage> GetAll();
        ProductImage FindProductImageById(long id);
        bool AddProductImage(ProductImage productImage);
        bool UpdateProductImage(ProductImage productImage);
    }
}
