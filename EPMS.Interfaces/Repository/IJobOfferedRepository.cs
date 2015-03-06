﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IJobOfferedRepository : IBaseRepository<JobOffered, long>
    {
        List<JobOffered> GetJobsOfferedByJobTitleId(long jobTitleId);
        IEnumerable<JobOffered> GetRecentJobOffereds();
    }
}