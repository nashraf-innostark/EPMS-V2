using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IItemReleaseFormService
    {
        IRFCreateResponse GetCreateResponse(long id);
    }
}
