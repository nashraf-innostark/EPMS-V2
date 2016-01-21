using System.Globalization;

namespace EPMS.WebModels.ModelMappers
{
    public static class SizeMapper
    {
        public static WebsiteModels.Size CreateFromServerToClient(this Models.DomainModels.Size source)
        {
            return new WebsiteModels.Size
            {
                SizeId = source.SizeId,
                SizeNameEn=source.SizeNameEn,
                SizeNameAr = source.SizeNameAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
        public static WebsiteModels.Size CreateForSizeList(this Models.DomainModels.Size source)
        {
            return new WebsiteModels.Size
            {
                SizeId = source.SizeId,
                SizeNameEn = source.SizeNameEn,
                SizeNameAr = source.SizeNameAr,
            };
        }
    }
}