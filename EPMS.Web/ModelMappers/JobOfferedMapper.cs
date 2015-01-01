﻿using ApiModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class JobOfferedMapper
    {
        public static Models.JobOffered CreateFrom(this DomainModels.JobOffered source)
        {
            return new Models.JobOffered
            {
                JobOfferedId = source.JobOfferedId,
                JobTitleId = source.JobTitleId,
                BasicSalary = source.BasicSalary,
                IsOpen = source.IsOpen,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        public static DomainModels.JobOffered CreateFrom(this Models.JobOffered source)
        {
            return new DomainModels.JobOffered
            {
                JobOfferedId = source.JobOfferedId,
                JobTitleId = source.JobTitleId,
                BasicSalary = source.BasicSalary,
                IsOpen = source.IsOpen,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
    }
}