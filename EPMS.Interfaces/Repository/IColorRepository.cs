using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IColorRepository : IBaseRepository<Color, long>
    {
        bool ColorExists(Color color);
    }
}
