using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class ProjectMapper
    {
        public static Project CreateFromClientToServer(this Models.Project source)
        {
            return new Project
            {
                CustomerId = source.CustomerId,
                OrderId = source.OrderId,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecLastUpdatedBy = source.RecLastUpdatedBy
            };
        }
        public static Models.Project CreateFromServerToClient(this Project source)
        {
            return new Models.Project
            {
                CustomerId = source.CustomerId
            };
        }
        //public static DashboardModels.Project CreateForDashboard(this Project source)
        //{
        //    return new DashboardModels.Project
        //    {
        //    };
        //}
    }
}