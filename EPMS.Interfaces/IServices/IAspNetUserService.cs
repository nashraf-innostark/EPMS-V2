using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IAspNetUserService
    {
        AspNetUser FindById(string id);
    }
}
