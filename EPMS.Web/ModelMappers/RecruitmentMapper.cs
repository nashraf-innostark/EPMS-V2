﻿using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class RecruitmentMapper
    {
        #region Recruitment Mappers
        public static JobOffered CreateFromClientToServer(this Models.JobOffered source)
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
                BasicSalary = source.BasicSalary,
                NoOfPosts = source.NoOfPosts,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        public static Models.JobOffered CreateFromServerToClient(this JobOffered source)
        {
            return new Models.JobOffered
            {
                JobOfferedId = source.JobOfferedId,
                JobTitleId = source.JobTitleId,
                TitleE = source.TitleE,
                TitleA = source.TitleA,
                DescriptionE = source.DescriptionE,
                DescriptionA = source.DescriptionA,
                IsOpen = source.IsOpen,
                BasicSalary = source.BasicSalary,
                NoOfPosts = source.NoOfPosts,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Department = source.Department.CreateFrom()
            };
        }
        #endregion
    }
}