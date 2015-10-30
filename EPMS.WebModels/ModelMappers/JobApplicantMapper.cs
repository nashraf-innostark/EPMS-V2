using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EPMS.WebModels.ModelMappers
{
    public static class JobApplicantMapper
    {
        public static WebsiteModels.JobApplicant CreateFrom(this Models.DomainModels.JobApplicant source)
        {
            return new WebsiteModels.JobApplicant
            {
                JobOfferedId = source.JobOfferedId,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                LastUpdatedBy = source.LastUpdatedBy,
                LastUpdatedDate = source.LastUpdatedDate
            };
        }

        public static Models.DomainModels.JobApplicant CreateFrom(this WebsiteModels.JobApplicant source, List<WebsiteModels.ApplicantQualification> qualifications, List<WebsiteModels.ApplicantExperience> experiences)
        {
            Models.DomainModels.JobApplicant retVal = new Models.DomainModels.JobApplicant();
            retVal.ApplicantId = source.ApplicantId;
            retVal.ApplicantFirstNameE = source.ApplicantFirstNameE;
            retVal.ApplicantFirstNameA = source.ApplicantFirstNameA;
            retVal.ApplicantMiddleNameE = source.ApplicantMiddleNameE;
            retVal.ApplicantMiddleNameA = source.ApplicantMiddleNameA;
            retVal.ApplicantNationality = source.ApplicantNationality;
            retVal.JobOfferedId = source.JobOfferedId;
            retVal.ApplicantFamilyNameE = source.ApplicantFamilyNameE;
            retVal.ApplicantFamilyNameA = source.ApplicantFamilyNameA;
            retVal.NoOfFamilyMembers = source.NoOfFamilyMembers;
            retVal.DepartmentId = source.DepartmentId;
            retVal.ApplicantSex = source.ApplicantSex;
            retVal.DateOfBirth = !string.IsNullOrEmpty(source.DateOfBirth)
                ? DateTime.ParseExact(source.DateOfBirth, "dd/MM/yyyy", new CultureInfo("en"))
                : (DateTime?) null;
            retVal.PlaceOfBirth = source.PlaceOfBirth;
            retVal.PassportNumber = source.PassportNumber;
            retVal.LanguagesKnown = source.LanguagesKnown;
            retVal.EmployedNow = source.EmployedNow;
            retVal.GetMonthlyPayment = source.GetMonthlyPayment;
            retVal.GovernmentEmployeeEver = source.GovernmentEmployeeEver;
            retVal.GovernmentAreaWorked = source.GovernmentAreaWorked;
            retVal.GovernmentJobOfficial = source.GovernmentJobOfficial;
            retVal.ReasonOfLeaving = source.ReasonOfLeaving;
            retVal.JobLeavingDate = !string.IsNullOrEmpty(source.JobLeavingDate)
                ? DateTime.ParseExact(source.JobLeavingDate, "dd/MM/yyyy", new CultureInfo("en"))
                : (DateTime?) null;
            retVal.AcceptAgreement = source.AcceptAgreement;
            retVal.Email = source.Email;
            retVal.MobileNumber = source.MobileNumber;
            retVal.CreatedBy = source.CreatedBy;
            retVal.CreatedDate = source.CreatedDate;
            retVal.LastUpdatedBy = source.LastUpdatedBy;
            retVal.LastUpdatedDate = source.LastUpdatedDate;
            retVal.ApplicantQualifications = qualifications.Select(x => x.CreateFromClientToServer()).ToList();
            retVal.ApplicantExperiences = experiences.Select(x => x.CreateFromClientToServer()).ToList();
            return retVal;
        }

        /// <summary>
        /// To Convert Job Applicant Model in Applicant Model for Job Applicants List
        /// </summary>
        public static WebsiteModels.ApplicantModel CreateFromApplicant(this Models.DomainModels.JobApplicant source)
        {
            return new WebsiteModels.ApplicantModel()
            {
                ApplicantName = source.ApplicantFirstNameE + " " + source.ApplicantMiddleNameE,
                JobApplicantId = source.ApplicantId,
                ApplicantMobile = source.MobileNumber,
                ApplicantEmail = source.Email,
                JobOffered = source.JobOffered.JobTitle.JobTitleNameE,
                DepartmentName = source.JobOffered.JobTitle.Department.DepartmentNameE
            };
        }

        public static WebsiteModels.JobApplicant CreateJobApplicant(this Models.DomainModels.JobApplicant source)
        {
            return new WebsiteModels.JobApplicant
            {
                ApplicantId = source.ApplicantId,
                ApplicantFirstNameE = source.ApplicantFirstNameE,
                ApplicantFirstNameA = source.ApplicantFirstNameA,
                ApplicantMiddleNameE = source.ApplicantMiddleNameE,
                ApplicantMiddleNameA = source.ApplicantMiddleNameA,
                ApplicantNationality = source.ApplicantNationality,
                JobOfferedId = source.JobOfferedId,
                ApplicantFamilyNameE = source.ApplicantFamilyNameE,
                ApplicantFamilyNameA = source.ApplicantFamilyNameA,
                NoOfFamilyMembers = source.NoOfFamilyMembers,
                DepartmentId = source.DepartmentId,
                ApplicantSex = source.ApplicantSex,
                DateOfBirth = source.DateOfBirth != null ? Convert.ToDateTime(source.DateOfBirth).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                PlaceOfBirth = source.PlaceOfBirth,
                PassportNumber = source.PassportNumber,
                LanguagesKnown = source.LanguagesKnown,
                EmployedNow = source.EmployedNow,
                GetMonthlyPayment = source.GetMonthlyPayment,
                GovernmentEmployeeEver = source.GovernmentEmployeeEver,
                GovernmentAreaWorked = source.GovernmentAreaWorked,
                GovernmentJobOfficial = source.GovernmentJobOfficial,
                ReasonOfLeaving = source.ReasonOfLeaving,
                JobLeavingDate = source.JobLeavingDate != null ? Convert.ToDateTime(source.JobLeavingDate).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                AcceptAgreement = source.AcceptAgreement,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                LastUpdatedBy = source.LastUpdatedBy,
                LastUpdatedDate = source.LastUpdatedDate,
                DepartmentNameE = source.JobOffered.JobTitle.Department.DepartmentNameE,
                DepartmentNameA = source.JobOffered.JobTitle.Department.DepartmentNameA,
                JobDescriptionE = source.JobOffered.JobTitle.JobTitleNameE,
                JobDescriptionA = source.JobOffered.JobTitle.JobTitleNameA,
                Email = source.Email,
                MobileNumber = source.MobileNumber
            };
        }

        public static WebsiteModels.ContactList CreateForContactList(this Models.DomainModels.JobApplicant source)
        {
            return new WebsiteModels.ContactList
            {
                Link = "HR/JobApplicant/Detail/" + source.ApplicantId,
                NameE = source.ApplicantFirstNameE + " " + source.ApplicantMiddleNameE ?? "",
                NameA = source.ApplicantFirstNameA + " " + source.ApplicantMiddleNameA ?? "",
                Type = "Applicant",
                MobileNumber = source.MobileNumber,
                Email = source.Email,
            };

        }

        public static Models.DomainModels.ApplicantQualification CreateFromClientToServer(
            this WebsiteModels.ApplicantQualification source)
        {
            return new Models.DomainModels.ApplicantQualification
            {
                QualificationId = source.QualificationId,
                ApplicantId = source.ApplicantId,
                Certificate = source.Certificate,
                CertificateDate = !string.IsNullOrEmpty(source.CertificateDate) ? DateTime.ParseExact(source.CertificateDate, "dd/MM/yyyy", new CultureInfo("en")) : (DateTime?)null,
                CollegeSchoolName = source.CollegeSchoolName,
                Field = source.Field,
                NoOfYears = source.NoOfYears,
                Notes = source.Notes,
                PlaceOfStudy = source.PlaceOfStudy,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                LastUpdatedBy = source.LastUpdatedBy,
                LastUpdatedDate = source.LastUpdatedDate
            };
        }
        public static WebsiteModels.ApplicantQualification CreateFromServerToClient(this Models.DomainModels.ApplicantQualification source)
        {
            return new WebsiteModels.ApplicantQualification
            {
                QualificationId = source.QualificationId,
                ApplicantId = source.ApplicantId,
                Certificate = source.Certificate,
                CertificateDate = source.CertificateDate != null ? Convert.ToDateTime(source.CertificateDate).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                CollegeSchoolName = source.CollegeSchoolName,
                Field = source.Field,
                NoOfYears = source.NoOfYears,
                Notes = source.Notes,
                PlaceOfStudy = source.PlaceOfStudy,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                LastUpdatedBy = source.LastUpdatedBy,
                LastUpdatedDate = source.LastUpdatedDate
            };
        }
        public static WebsiteModels.ApplicantExperience CreateFromServerToClient(this Models.DomainModels.ApplicantExperience source)
        {
            return new WebsiteModels.ApplicantExperience
            {
                ExperienceId = source.ExperienceId,
                ApplicantId = source.ApplicantId,
                CompanyName = source.CompanyName,
                FromDate = source.FromDate != null ? Convert.ToDateTime(source.FromDate).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                ToDate = source.ToDate != null ? Convert.ToDateTime(source.ToDate).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                JobTitle = source.JobTitle,
                Position = source.Position,
                TypeOfWork = source.TypeOfWork,
                Salary = source.Salary,
                ReasonToLeave = source.ReasonToLeave,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                LastUpdatedBy = source.LastUpdatedBy,
                LastUpdatedDate = source.LastUpdatedDate
            };
        }
        public static Models.DomainModels.ApplicantExperience CreateFromClientToServer(
            this WebsiteModels.ApplicantExperience source)
        {
            return new Models.DomainModels.ApplicantExperience
            {
                ExperienceId = source.ExperienceId,
                ApplicantId = source.ApplicantId,
                CompanyName = source.CompanyName,
                FromDate = !string.IsNullOrEmpty(source.FromDate) ? DateTime.ParseExact(source.FromDate, "dd/MM/yyyy", new CultureInfo("en")) : (DateTime?)null,
                ToDate = !string.IsNullOrEmpty(source.ToDate) ? DateTime.ParseExact(source.ToDate, "dd/MM/yyyy", new CultureInfo("en")) : (DateTime?)null,
                JobTitle = source.JobTitle,
                Position = source.Position,
                TypeOfWork = source.TypeOfWork,
                Salary = source.Salary,
                ReasonToLeave = source.ReasonToLeave,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                LastUpdatedBy = source.LastUpdatedBy,
                LastUpdatedDate = source.LastUpdatedDate
            };
        }
    }
}