using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IPurchaseOrderService
    {
        POCreateResponse GetPoResponseData(long? id);
    }
}
