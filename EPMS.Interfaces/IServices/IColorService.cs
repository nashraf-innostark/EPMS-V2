using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface  IColorService
    {
        IEnumerable<Color> GetAll();
        Color FindColorById(long id);
        bool AddColor(Color color);
        bool UpdateColor(Color color);
        void DeleteColor(Color color);

    }
}
