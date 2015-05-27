using EPMS.Web.Models;
using EPMS.Web.ViewModels.RFI;
namespace EPMS.Web.ModelMappers.Inventory.RFI
{
    public static class RFIMapper
    {
        public static EPMS.Models.DomainModels.RFI CreateRFIClientToServer(this RFIViewModel source)
        {
            var rfiItem = new EPMS.Models.DomainModels.RFI
            {
                RFIId = source.Rfi.RFIId,
                OrderId = source.Rfi.OrderId,
                UsageE = source.Rfi.UsageE,
                UsageA = source.Rfi.UsageA,
                RecCreatedBy = source.Rfi.RecCreatedBy,
                RecCreatedDate = source.Rfi.RecCreatedDate,
                RecUpdatedBy = source.Rfi.RecCreatedBy,
                RecUpdatedDate = source.Rfi.RecUpdatedDate,


            };
            return null;
        }

        public static EPMS.Models.DomainModels.RFIItem CreateRFIItemClientToServer(this RFIItem source)
        {
            var rfiItem = new EPMS.Models.DomainModels.RFIItem
            {
                RFIItemId = source.RFIItemId,
                RFIId = source.RFIId,
                ItemVariationId = source.ItemVariationId,
                
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecCreatedBy,
                RecUpdatedDate = source.RecUpdatedDate,


            };
            return null;
        }
    }
}