using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProductSectionService
    {
        IEnumerable<ProductSection> GetAll();
        ProductSection FindProductSectionById(long id);
        bool AddProductSection(ProductSection productSection);
        bool UpdateProductSection(ProductSection productSection);
        void DeleteProductSection(ProductSection productSection);
        bool Delete(long id);
    }
}
