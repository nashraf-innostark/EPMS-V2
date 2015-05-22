using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface ISizeRepository : IBaseRepository<Size, long>
    {
        bool SizeExists(Size size);
    }
}
