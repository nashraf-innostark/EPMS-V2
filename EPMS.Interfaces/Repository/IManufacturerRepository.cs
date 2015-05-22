using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IManufacturerRepository : IBaseRepository<Manufacturer, long>
    {
        bool ManufacturerExists(Manufacturer manufacturer);
    }
}
