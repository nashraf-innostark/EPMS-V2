﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProjectTaskRepository : IBaseRepository<ProjectTask, long>
    {
        IEnumerable<ProjectTask> GetTasksByProjectId(long projectId);
        IEnumerable<ProjectTask> FindProjectTaskByProjectId(long projectid);

        ProjectTask FindTaskWithPreRequisites(long id);

    }
}
