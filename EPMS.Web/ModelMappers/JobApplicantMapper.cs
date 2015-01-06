using System.Web.Http.ModelBinding;
using ApiModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class JobApplicantMapper
    {
        public static Models.JobApplicant CreateFrom(this DomainModels.JobApplicant source)
        {
            return new Models.JobApplicant
            {
                JobOfferedId = source.JobOfferedId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        public static DomainModels.JobApplicant CreateFrom(this Models.JobApplicant source)
        {
            return new DomainModels.JobApplicant
            {
                ApplicantName = source.ApplicantName,
                ApplicantMobile = source.ApplicantMobile,
                ApplicantEmail = source.ApplicantEmail,
                ApplicantAge = source.ApplicantAge,
                MaritalStatus = source.MaritalStatus,
                Nationality = source.Nationality,
                IqamaOrNationalIdNo = source.IqamaOrNationalIdNo,
                DrivingLicense = source.DrivingLicense,
                ApplicantCvPath = source.ApplicantCvPath,
                JobOfferedId = source.JobOfferedId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        /// <summary>
        /// To Convert Job Applicant Model in Applicant Model for Job Applicants List
        /// </summary>
        public static Models.ApplicantModel CreateFromApplicant(this DomainModels.JobApplicant source)
        {
            return new Models.ApplicantModel()
            {
                ApplicantName = source.ApplicantName,
                JobApplicantId = source.ApplicantId,
                ApplicantMobile = source.ApplicantMobile,
                ApplicantEmail = source.ApplicantEmail,
                JobOffered = source.JobOffered.JobTitle.JobTitleNameE,
                DepartmentName = source.JobOffered.JobTitle.Department.DepartmentNameE
            };
        }

        public static Models.JobApplicant CreateJobApplicant(this DomainModels.JobApplicant source)
        {
            return new Models.JobApplicant
            {
                ApplicantName = source.ApplicantName,
                ApplicantMobile = source.ApplicantMobile,
                ApplicantEmail = source.ApplicantEmail,
                ApplicantAge = source.ApplicantAge,
                MaritalStatus = source.MaritalStatus,
                Nationality = source.Nationality,
                IqamaOrNationalIdNo = source.IqamaOrNationalIdNo,
                DrivingLicense = source.DrivingLicense,
                ApplicantCvPath = source.ApplicantCvPath,
                JobOfferedId = source.JobOfferedId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                DepartmentNameE = source.JobOffered.JobTitle.Department.DepartmentNameE,
                DepartmentNameA = source.JobOffered.JobTitle.Department.DepartmentNameA,
                JobDescriptionE = source.JobOffered.JobTitle.JobTitleNameE,
                JobDescriptionA = source.JobOffered.JobTitle.JobTitleNameA
            };
        }
    }
}