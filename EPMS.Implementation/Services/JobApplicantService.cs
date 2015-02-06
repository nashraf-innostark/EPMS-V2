using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Implementation.Services
{
    public class JobApplicantService : IJobApplicantService
    {
        private readonly INotificationService notificationService;
        private readonly IJobApplicantRepository jobApplicantRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationService"></param>
        /// <param name="jobApplicantRepository"></param>
        public JobApplicantService(INotificationService notificationService,IJobApplicantRepository jobApplicantRepository)
        {
            this.notificationService = notificationService;
            this.jobApplicantRepository = jobApplicantRepository;
        }

        #endregion

        public IEnumerable<JobApplicant> GetAll()
        {
            return jobApplicantRepository.GetAll();
        }

        public JobApplicantResponse GetJobApplicants(JobApplicantSearchRequest jobApplicantSearchRequest)
        {
            return jobApplicantRepository.GetAllJobApplicants(jobApplicantSearchRequest);
        }

        public JobApplicant FindJobApplicantById(long id)
        {
            return jobApplicantRepository.Find((int)id);
        }

        public bool AddJobApplicant(JobApplicant jobApplicant)
        {
            try
            {
                jobApplicantRepository.Add(jobApplicant);
                jobApplicantRepository.SaveChanges();
                SendNotification(jobApplicant);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public void SendNotification(JobApplicant jobApplicant)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();

            #region Send notification to admin

            notificationViewModel.NotificationResponse.TitleE = "An applicant applied for a job.";
            notificationViewModel.NotificationResponse.TitleA = "An applicant applied for a job.";

            notificationViewModel.NotificationResponse.CategoryId = 5; //Other
            notificationViewModel.NotificationResponse.AlertBefore = 3; //1 Day
            notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.AddDays(-1).ToShortDateString();
            notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
            notificationViewModel.NotificationResponse.SystemGenerated = true;

            notificationService.AddUpdateNotification(notificationViewModel);

            #endregion
        }
    }
}
