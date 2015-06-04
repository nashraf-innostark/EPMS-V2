using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IItemReleaseService
    {
        IRFCreateResponse GetCreateResponse(long id);
    }
}
