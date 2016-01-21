using System.Globalization;

namespace EPMS.WebModels.ModelMappers
{
    public static class StatusMapper
    {
        public static WebsiteModels.Status CreateFromServerToClient(this Models.DomainModels.Status source)
        {
            return new WebsiteModels.Status
            {
                StatusId = source.StatusId,
                StatusNameEn = source.StatusNameEn,
                StatusNameAr = source.StatusNameAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}