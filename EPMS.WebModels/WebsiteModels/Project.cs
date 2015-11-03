using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class Project
    {
        public long ProjectId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.PMS.Project), ErrorMessageResourceName = "ProjectNameERequiredMsg")]
        public string NameE { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.PMS.Project), ErrorMessageResourceName = "ProjectNameARequiredMsg")]
        public string NameA { get; set; }
        public long? CustomerId { get; set; }
        public long? OrderId { get; set; }
        public long? QuotationId { get; set; }
        public string SerialNo { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.PMS.Project), ErrorMessageResourceName = "ProjectStartDateRequiredMsg")]
        public string StartDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.PMS.Project), ErrorMessageResourceName = "ProjectEndDateRequiredMsg")]
        public string EndDate { get; set; }
        public int Status { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        public string NotesForCustomerE { get; set; }
        public string NotesForCustomerA { get; set; }
        public string RecCreatedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public string CustomerNameE { get; set; }
        public string CustomerNameA { get; set; }
        public decimal Price { get; set; }
        public decimal TotalTasksCost { get; set; }
        public decimal OtherCost { get; set; }
        public decimal Profit { get; set; }
        public double ProgressTotal { get; set; }
    }
}