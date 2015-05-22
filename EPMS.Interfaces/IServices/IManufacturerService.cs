using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface  IManufacturerService
    {
        IEnumerable<Manufacturer> GetAll();
        Manufacturer FindManufacturerById(long id);
        bool AddManufacturer(Manufacturer manufacturer);
        bool UpdateManufacturer(Manufacturer manufacturer);
        void DeleteManufacturer(Manufacturer manufacturer);
    }
}
