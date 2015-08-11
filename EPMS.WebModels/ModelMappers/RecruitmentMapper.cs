using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class RecruitmentMapper
    {
        #region Recruitment Mappers
        public static JobOffered CreateFromClientToServer(this WebsiteModels.JobOffered source)
        {
            return new JobOffered
            {
                JobOfferedId = source.JobOfferedId,
                JobTitleId = source.JobTitleId,
                TitleE = source.TitleE,
                TitleA = source.TitleA,
                DescriptionE = source.DescriptionE,
                DescriptionA = source.DescriptionA,
                IsOpen = source.IsOpen,
                ShowBasicSalary = source.ShowBasicSalary,
                NoOfPosts = source.NoOfPosts,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        public static WebsiteModels.JobOffered CreateFromServerToClient(this JobOffered source)
        {
            return new WebsiteModels.JobOffered
            {
                JobOfferedId = source.JobOfferedId,
                JobTitleId = source.JobTitleId,
                TitleE = source.TitleE,
                TitleA = source.TitleA,
                DescriptionE = source.DescriptionE,
                DescriptionA = source.DescriptionA,
                IsOpen = source.IsOpen,
                ShowBasicSalary = source.ShowBasicSalary,
                NoOfPosts = source.NoOfPosts,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                //Department = source.Department.CreateFrom()
            };
        }
        #endregion
    }
}