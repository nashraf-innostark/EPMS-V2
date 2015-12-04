using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
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
        private readonly IApplicantQualificationRepository applicantQualificationRepository;
        private readonly IApplicantExperienceRepository applicantExperienceRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationService"></param>
        /// <param name="jobApplicantRepository"></param>
        public JobApplicantService(INotificationService notificationService,IJobApplicantRepository jobApplicantRepository, IApplicantQualificationRepository applicantQualificationRepository, IApplicantExperienceRepository applicantExperienceRepository)
        {
            this.notificationService = notificationService;
            this.jobApplicantRepository = jobApplicantRepository;
            this.applicantQualificationRepository = applicantQualificationRepository;
            this.applicantExperienceRepository = applicantExperienceRepository;
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
                //// Save Applicant Qualification
                //foreach (var applicantQualification in jobApplicant.ApplicantQualifications)
                //{
                //    if (IsNotNullOrEmptyQualification(applicantQualification))
                //    {
                //        applicantQualification.ApplicantId = jobApplicant.ApplicantId;
                //        applicantQualificationRepository.Add(applicantQualification);
                //        applicantQualificationRepository.SaveChanges();
                //    }
                //}
                //// Save Applicant Experience
                //foreach (var applicantExperience in jobApplicant.ApplicantExperiences)
                //{
                //    if (IsNotNullOrEmptyExperience(applicantExperience))
                //    {
                //        applicantExperience.ApplicantId = jobApplicant.ApplicantId;
                //        applicantExperienceRepository.Add(applicantExperience);
                //        applicantExperienceRepository.SaveChanges();
                //    }
                //}
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

            notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["JobApplicationE"];
            notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["JobApplicationA"];
            notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["JobApplicationAlertBefore"]); //Days

            notificationViewModel.NotificationResponse.CategoryId = 6; //Other
            notificationViewModel.NotificationResponse.SubCategoryId = jobApplicant.JobOfferedId;
            notificationViewModel.NotificationResponse.ItemId = jobApplicant.ApplicantId;
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
            
            notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
            notificationViewModel.NotificationResponse.SystemGenerated = true;
            notificationViewModel.NotificationResponse.ForAdmin = true;
            notificationViewModel.NotificationResponse.ForRole = UserRole.Admin;

            notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);

            #endregion
        }

        public bool IsNotNullOrEmptyQualification(ApplicantQualification qualification)
        {
            if (!string.IsNullOrEmpty(qualification.Certificate) || !string.IsNullOrEmpty(qualification.Field) ||
                !string.IsNullOrEmpty(qualification.PlaceOfStudy) ||
                !string.IsNullOrEmpty(qualification.CollegeSchoolName) || !string.IsNullOrEmpty(qualification.NoOfYears) ||
                !string.IsNullOrEmpty(qualification.Notes))
            {
                return true;
            }
            return false;
        }

        public bool IsNotNullOrEmptyExperience(ApplicantExperience experience)
        {
            if (!string.IsNullOrEmpty(experience.CompanyName) || !string.IsNullOrEmpty(experience.JobTitle) || !string.IsNullOrEmpty(experience.Position) || experience.Salary != 0 || !string.IsNullOrEmpty(experience.TypeOfWork) || !string.IsNullOrEmpty(experience.ReasonToLeave))
            {
                return true;
            }
            return false;
        }
    }
}
