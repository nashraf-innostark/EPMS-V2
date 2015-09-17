using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProductSectionRepository : IBaseRepository<ProductSection, long>
    {
        bool ProductSectionExists(ProductSection productSection);
        ProductSection FindByDepartmentId(long departmentId);
    }
}
