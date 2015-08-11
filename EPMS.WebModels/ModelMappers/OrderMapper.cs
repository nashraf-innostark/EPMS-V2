using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class OrderMapper
    {
        public static AvailableOrdersDDL CreateForDDL(this Order source)
        {
            return new AvailableOrdersDDL
            {
                OrderId = source.OrderId,
                OrderNo = source.OrderNo
            };
        }
    }
}
