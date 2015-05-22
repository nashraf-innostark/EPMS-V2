using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface  ISizeService
    {
        IEnumerable<Size> GetAll();
        Size FindSizeById(long id);
        bool AddSize(Size size);
        bool UpdateSize(Size size);
        void DeleteSize(Size size);

    }
}
