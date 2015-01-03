using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class JobApplicantService : IJobApplyService
    {
        public IEnumerable<JobApplicant> GetAll()
        {
            throw new NotImplementedException();
        }

        public JobApplicant FindJobApplicantById(long id)
        {
            throw new NotImplementedException();
        }

        public bool AddJobApplicant(JobApplicant jobTitle)
        {
            throw new NotImplementedException();
        }

        public List<JobApplicant> GetJobsOfferedByJobTitleId(long jobTitleId)
        {
            throw new NotImplementedException();
        }
    }
}
