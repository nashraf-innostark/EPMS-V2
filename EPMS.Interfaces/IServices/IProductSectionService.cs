using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

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
