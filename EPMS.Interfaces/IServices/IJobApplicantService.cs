using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IJobApplicantService
    {
        IEnumerable<JobApplicant> GetAll();
        JobApplicant FindJobApplicantById(long id);
        bool AddJobApplicant(JobApplicant jobApplicant);

        //bool UpdateJobOffered(JobApplicant jobTitle);
    }
}
