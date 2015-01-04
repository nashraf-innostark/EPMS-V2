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
                JobOfferedId = source.JobOfferedId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
    }
}