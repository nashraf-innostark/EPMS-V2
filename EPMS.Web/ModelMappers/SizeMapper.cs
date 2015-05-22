using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class SizeMapper
    {
        public static WebModels.Size CreateFromServerToClient(this DomainModels.Size source)
        {
            return new WebModels.Size
            {
                SizeId = source.SizeId,
                SizeNameEn=source.SizeNameEn,
                SizeNameAr = source.SizeNameAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}