using EPMS.Models.RequestModels;
using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ColorMapper
    {
        public static WebModels.Color CreateFromServerToClient(this DomainModels.Color source)
        {
            return new WebModels.Color
            {
                ColorId = source.ColorId,
                ColorNameEn = source.ColorNameEn,
                ColorNameAr = source.ColorNameAr,
                ColorCode = source.ColorCode,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}