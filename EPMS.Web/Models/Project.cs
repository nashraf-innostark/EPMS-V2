using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
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
        public string SerialNo { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        public string NotesForCustomerE { get; set; }
        public string NotesForCustomerA { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public string CustomerNameE { get; set; }
        public string CustomerNameA { get; set; }
        public double Price { get; set; }
        public int Progress { get; set; }
    }
}