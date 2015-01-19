using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class ProjectMapper
    {
        public static Project CreateFromClientToServer(this Models.Project source)
        {
            return new Project
            {
                NameE = source.NameE,
                NameA = source.NameA,
                CustomerId = source.CustomerId,
                OrderId = source.OrderId,
                SerialNo = source.SerialNo,
                DescriptionE = source.DescriptionE,
                DescriptionA = source.DescriptionA,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Status = source.Status,
                NotesE = source.NameE,
                NotesA = source.NameA,
                NotesForCustomerE = source.NotesForCustomerE,
                NotesForCustomerA = source.NotesForCustomerA,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
        public static Models.Project CreateFromServerToClient(this Project source)
        {
            Models.Project project=new Models.Project();
            project.ProjectId = source.ProjectId;
            project.NameE = source.NameE;
            project.NameA = source.NameA;
            project.CustomerId = source.CustomerId;
            if (source.CustomerId > 0)
            {
                project.CustomerNameE = source.Customer.CustomerNameE;
                project.CustomerNameA = source.Customer.CustomerNameA;
            }
            project.StartDate = source.StartDate;
            project.EndDate = source.EndDate;
            
            return project;
        }
        //public static DashboardModels.Project CreateForDashboard(this Project source)
        //{
        //    return new DashboardModels.Project
        //    {
        //    };
        //}
    }
}