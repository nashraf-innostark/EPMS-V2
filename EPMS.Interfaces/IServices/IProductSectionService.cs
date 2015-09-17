using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProductSectionService
    {
        IEnumerable<ProductSection> GetAll();
        ProductSection FindProductSectionById(long id);
        IList<long> RemoveDuplication(string[] departmentIds);
        bool AddProductSection(ProductSection productSection);
        bool UpdateProductSection(ProductSection productSection);
        void DeleteProductSection(ProductSection productSection);
        string DeleteProductSection(long id);
        bool SaveProductSections(IList<ProductSection> productSections);
    }
}
