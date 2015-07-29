using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProductSectionRepository : IBaseRepository<ProductSection, long>
    {
        bool ProductSectionExists(ProductSection productSection);
    }
}
